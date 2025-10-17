using IdentityServer.Domain.Entities;
using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.CreateUser
{
    public record CreateSignupUserCommand : IRequest<Result<Guid?>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }

        public ApplicationUser MapToEntity()
        {
            return new ApplicationUser
            {
                UserName = this.Username,
                NormalizedUserName = this.Username,
                FirstName = this.Firstname,
                LastName = this.Lastname,
                PhoneNumber = this.PhoneNumber,
                IsActive = true,
            };
        }
    }
}
