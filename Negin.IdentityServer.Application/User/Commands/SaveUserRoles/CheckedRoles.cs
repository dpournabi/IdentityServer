using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.User.Commands.SaveUserRoles
{
    public record CheckedRoles
    {
        public Guid Id { get; set; }
        public bool Checked { get; set; }
    }
}
