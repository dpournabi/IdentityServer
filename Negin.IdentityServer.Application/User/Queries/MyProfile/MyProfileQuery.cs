using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.User.Queries.MyProfile;

public class MyProfileQuery : IRequest<Result<UserProfileResponse>>
{
    public string UserName { get; set; }
}
