using Microsoft.AspNetCore.Mvc;
using CPBApi.Helpers;
using System.Linq;
using CPBApi.Services;
using CPBApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CPBApi.Controllers
{
    [Route("api/[controller]")]
    public class CBPController : ControllerBase
    {
        private IAuthService _service;
        public CBPController(IAuthService service)
        {
            _service = service;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public IActionResult CalculateCBP([FromBody] OrderLineModelBinding[] target)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrderLine[] orderLines =
                target.Select(x => x.GetOrderLine()).ToArray();

            foreach (OrderLine line in orderLines)
            {
                line.Amount_fx = CalculateAmount(line);
                line.Tiers = null;
            }

            return Ok(orderLines);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("identity")]
        public IActionResult Identity([FromBody] JwtRequest request)
        {

            var authentification = _service.Authenticate(request);

            if (authentification is null)
                return BadRequest(new { message = "Wrong Id and password combination" });

            return Ok(authentification);
        }

        private double CalculateAmount(OrderLine line)
        {
            //This ensure the Price tiers are in ascending order
            line.Tiers.Sort((a, b) => a.fromValue.CompareTo(b.fromValue));

            double amount = 0;
            foreach (PriceTier tier in line.Tiers)
            {
                double diff;
                if (line.Consumption_fx > tier.toValue)
                {
                    diff = tier.toValue - tier.fromValue;

                    if (tier.fromValue > 0) diff++;

                    amount += diff * tier.unitPriceC;
                }
                else
                {
                    diff = line.Consumption_fx - tier.fromValue + 1;
                    amount += diff * tier.unitPriceC;
                    break;
                }
            }

            return amount;
        }
    }
}
