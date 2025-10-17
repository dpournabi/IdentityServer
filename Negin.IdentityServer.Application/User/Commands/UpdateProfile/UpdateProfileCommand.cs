using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<Result>
    {
        public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
