using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Authentication;

namespace UserApi.Test.Stubs
{
    public class AuthenticationManagerStub : IAuthenticationManager
    {
        public Task<AuthResult> AuthenticateUser(string userName, string secret)
        {
            if (userName.Equals("amritaz@abc.com"))
                return Task.FromResult(new AuthResult { Id = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), IsAuthenticated = true, Name = "Amritaz" });
            return Task.FromResult(new AuthResult { IsAuthenticated = false });
        }
    }
}
