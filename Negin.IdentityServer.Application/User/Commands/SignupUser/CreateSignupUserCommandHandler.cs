using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using IdentityServer.Domain.Events.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.User.Commands.CreateUser
{
    public class CreateSignupUserCommandHandler : IRequestHandler<CreateSignupUserCommand, Result<Guid?>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CreateSignupUserCommandHandler(IApplicationDbContext dbContext,
                                        IPasswordHasher<ApplicationUser> passwordHasher,
                                        SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        public async Task<Result<Guid?>> Handle(CreateSignupUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser entity = request.MapToEntity();

                IdentityResult createUser = await _signInManager.UserManager
                                                 .CreateAsync(entity, request.Password)
                                                 .ConfigureAwait(false);

                if (createUser.Succeeded)
                {
                    await _signInManager.UserManager.AddToRoleAsync(entity, "Client");

                    entity.AddDomainEvent(new UserCreatedEvent(entity));
                    return Result<Guid?>.Success(entity.Id);
                }
                else
                {
                    return Result<Guid?>.Failure(createUser.Errors.Select(e => e.Description).ToList(), null);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
