using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Core.Data.Entities;
using UserApi.Core.Data.Repositories;

namespace UserApi.Test.Stubs
{
    public class UserRepositoryStub : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
              
        public UserRepositoryStub()
        {
            _users = GetTestUsers();
        }
        public Task<Guid> Add(User user)
        {
            return Task.FromResult(Guid.NewGuid());
        }

        public Task<User> GetUserByName(string userName)
        {
            return Task.FromResult( _users.FirstOrDefault(u=>u.UserName.Equals(userName)));
        }

        public Task<List<User>> GetUsers()
        {
            return Task.FromResult(_users);
        }

        public Task<bool> IsDifferentUserWithSameUserNameExists(string userName, Guid id)
        {
            if (userName.Equals("amritaz@abc.com"))
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false); ;
        }

        public Task Update(User user)
        {
            return Task.CompletedTask;
        }

        private List<User> GetTestUsers()
        {
            return new List<User>(){
                new User { CreatedOn = DateTime.UtcNow, ModifiedOn = DateTime.UtcNow, FirstName = "Ams", LastName = "Dhange", Id = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), Password = "demo123", UserName = "amritaz@abc.com" },
                                        new User { CreatedOn = DateTime.UtcNow, ModifiedOn = DateTime.UtcNow, FirstName = "Prashant", LastName = "Zirkande", Id = new Guid("0f8fad5b-d9cb-469f-a165-70867728952e"), Password = "pass123", UserName = "demouser@pqr.com" },
                    new User { CreatedOn = DateTime.UtcNow, ModifiedOn = DateTime.UtcNow, FirstName = "Prashant", LastName = "Zirkande", Id = new Guid("0f8fad5b-d9cb-469f-a165-30867728950e"), Password = "xdssyhrjh9eww", UserName = "prashantd@xyz.com" }
                };
        }
    }
}
