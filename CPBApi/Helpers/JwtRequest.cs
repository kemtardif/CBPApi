
using System.ComponentModel.DataAnnotations;


namespace CPBApi.Helpers
{
    public class JwtRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Secret { get; set; }
     
    }
}
