using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.SignInUser
{
    public class SigninCommand : IRequest<Result<SigninResponse>>
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public string ClientId { get; init; }
        public string ClientSecret { get; init; }
        public bool RememberLogin { get; init; }
    }
}
