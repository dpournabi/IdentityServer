using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.UpdateUser;
public class ChangePasswordCommand : IRequest<Result>
{
    public string UserName { get; set; }
    //public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmationPassword { get; set; }
}
