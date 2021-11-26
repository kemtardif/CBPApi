using System.Collections.Generic;
using CPBApi.Helpers;


namespace CPBApi.Services
{
    public interface IAuthService
    {
        JwtResponse Authenticate(JwtRequest request);
        IEnumerable<Client> GetAll();
        Client GetById(string id);
    }
}
