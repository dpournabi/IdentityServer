using IdentityModel.Client;
using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using IdentityServer8.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nsp.Common;
using System.Net.Http.Headers;

namespace IdentityServer.Application.User.Commands.SignInUser
{
    public class SigninCommandHandler : IRequestHandler<SigninCommand, Result<SigninResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public SigninCommandHandler(IApplicationDbContext dbContext, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<Result<SigninResponse>> Handle(SigninCommand request, CancellationToken cancellationToken)
        {
            string message = string.Empty;
            Domain.Entities.ApplicationUser? user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (user is null)
            {
                return Result<SigninResponse>.Failure(new List<string> { "Invalid user name or password!" });
            }

            if (user is not { LockoutEnd: null })
            {
                return Result<SigninResponse>.Failure(new List<string> { "User is locked!" });
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberLogin, lockoutOnFailure: true).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return Result<SigninResponse>.Failure(new List<string> { "Invalid user name or password!" }, null);
            }

            SigninResponse tokenResponse = await GetAccessToken(request);

            return Result<SigninResponse>.Success(tokenResponse);
        }

        private async Task<SigninResponse> GetAccessToken(SigninCommand request)
        {
            HttpClientHandler handler = new() { UseDefaultCredentials = false };
            string identityServerUrl = $"{_configuration["IdentityServerAddress"]}/connect/token";
            //handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new(handler);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            TokenResponse requestPassword = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = identityServerUrl,
                UserName = request.UserName,
                Password = request.Password,
                ClientId = request.ClientId,
                GrantType = GrantType.ResourceOwnerPassword,
                ClientSecret = request.ClientSecret,
            });
            SigninResponse loginResponse = new()
            {
                AccessToken = requestPassword.AccessToken,
                //TokenType = requestPassword.TokenType,
                ExpireDate = DateTime.Now.AddSeconds(requestPassword.ExpiresIn),
                RefreshToken = requestPassword.RefreshToken,
            };

            return await Task.FromResult(loginResponse);
        }
    }
}
