using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.UpdateProfile
{
    public class UnlockUserCommand : IRequest<Result>
    {
        public string UserName { get; set; }
    }
}
