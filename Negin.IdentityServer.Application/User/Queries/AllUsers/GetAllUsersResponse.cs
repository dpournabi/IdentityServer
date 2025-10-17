using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.User.Queries.AllUsers
{
    public class GetAllUsersResponse
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Roles { get; set; }
        public bool IsActive { get; set; }
    }
}
