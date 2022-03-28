using UserApi.Core.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace UserApi.Core.Data.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserByName(string userName);
        Task<Guid> Add(User user);
        Task Update(User user);
        Task<bool> IsDifferentUserWithSameUserNameExists(string userName, Guid id); 
    }
    public class UserRepositiory : IUserRepository
    {
        private  List<User> Users { get; set; }
        public UserRepositiory()
        {
            Users = new List<User>();
        }
        public Task<bool> IsDifferentUserWithSameUserNameExists(string userName, Guid id)
        {
            return Task.FromResult( Users.Any(u => u.Id != id && u.UserName.ToLower().Equals(userName)));
        }
        public Task<Guid> Add(User user)
        {
            user.Id  = Guid.NewGuid();
             Users.Add(user);
            return Task.FromResult(user.Id);
        }

        public Task Update(User user)
        {
            if ( Users.Any(user => user.Id == user.Id))
            {
                 Users.RemoveAll(user => user.Id == user.Id);
                 Users.Add(user);
            }

            return Task.CompletedTask;
        }

        public Task<User> GetUserByName(string userName)
        {
            return Task.FromResult( Users.FirstOrDefault(user => user.UserName.ToLower().Equals(userName.ToLower())));
        }
        public Task<List<User>> GetUsers()
        {
            return Task.FromResult( Users);
        }
    }
}
