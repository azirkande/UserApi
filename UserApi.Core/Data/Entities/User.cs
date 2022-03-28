using System;

namespace UserApi.Core.Data.Entities
{
    public record User
    {
        public Guid Id { get;  set; }
        public string UserName { get;  set; }
        public string FirstName { get;  set;  }

        public string LastName { get;  set; }
        public string Password { get;  set; }

        public DateTime CreatedOn { get;  set; }
        public DateTime ModifiedOn { get;  set; }
    }
}
