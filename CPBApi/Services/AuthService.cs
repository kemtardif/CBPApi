using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CPBApi.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace CPBApi.Services
{
    public class AuthService : IAuthService
    {
        private List<Client> _clients = new List<Client>
        {
            new Client { Id = "SYSTEM", Secret = "SYSTEM" }
        };

        private readonly JwtSettings _jwtSettings;

        public AuthService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public JwtResponse Authenticate(JwtRequest request)
        {
            var client = _clients.SingleOrDefault(x => x.Id == request.Id && x.Secret == request.Secret);

            if (client is null) 
                return null;

            var token = GenerateToken(client);

            return new JwtResponse()
            { 
                Id = client.Id,
                Token = token
            };
        }

        public IEnumerable<Client> GetAll()
        {
            return _clients;
        }

        public Client GetById(string id)
        {
            return _clients.FirstOrDefault(x => x.Id == id);
        }

        private string GenerateToken(Client client)
        {
            var tokenHandler = 
                new JwtSecurityTokenHandler();
            var key =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));
            var credentials =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(_jwtSettings.Issuer, 
                _jwtSettings.Issuer, 
                expires : DateTime.UtcNow.AddDays(_jwtSettings.Expiration), 
                signingCredentials: credentials);

            return tokenHandler.WriteToken(token);
        }
    }
}
