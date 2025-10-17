using IdentityModel.Client;
using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Domain.Entities;
using IdentityServer8.Models;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nsp.Common;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace IdentityServer.Application.User.Commands.SignInUserByOtp
{
    public class SigninByOtpCommandHandler : IRequestHandler<SigninByOtpCommand, Result<SigninByOtpResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IRequestClient<EmailVerificationCodeContract> _emailVerificationCodeClient;
        private readonly IRequestClient<SmsVerificationCodeContract> _smsVerificationCodeClient;

        public SigninByOtpCommandHandler(IApplicationDbContext dbContext,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IRequestClient<EmailVerificationCodeContract> emailVerificationCodeClient,
            IRequestClient<SmsVerificationCodeContract> smsVerificationCodeClient)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailVerificationCodeClient = emailVerificationCodeClient;
            _smsVerificationCodeClient = smsVerificationCodeClient;
        }

        public async Task<Result<SigninByOtpResponse>> Handle(SigninByOtpCommand request, CancellationToken cancellationToken)
        {
            string message = string.Empty;
            Domain.Entities.ApplicationUser? user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (user is null)
            {
                return Result<SigninByOtpResponse>.Failure(new List<string> { "Invalid user name or password!" });
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberLogin, lockoutOnFailure: true).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return Result<SigninByOtpResponse>.Failure(new List<string> { "Invalid user name or password!" }, null);
            }

            SigninByOtpResponse tokenResponse = await GetAccessToken(request);
            await CreateUserOtpAsync(user, tokenResponse, cancellationToken);
            return Result<SigninByOtpResponse>.Success();
        }

        private async Task<Result> CreateUserOtpAsync(ApplicationUser user, SigninByOtpResponse signinResponse, CancellationToken cancellationToken)
        {
            Random rnd = new();
            int digit = rnd.Next(10000, 99999);
            string hashedDigit = BCrypt.Net.BCrypt.HashPassword(digit.ToString());

            Response<Result<string>> sendSmsNotificationResult = await _smsVerificationCodeClient.GetResponse<Result<string>>(new SmsVerificationCodeContract
            {
                Code = digit.ToString(),
                Mobile = user.PhoneNumber.PadLeft(11, '0'),
            });

            //string messageBody = $"کد احراز هویت دو عاملی شما {digit} می باشد";
            //Response<Result<string>> sendEmailNotificationResult = await _emailVerificationCodeClient.GetResponse<Result<string>>(new EmailVerificationCodeContract
            //{
            //    Subject = "کد احراز هویت دو مرحله ای",
            //    MessageBody = messageBody,
            //    Recipients = new string[] { user.Email }
            //});

            if (//!sendEmailNotificationResult.Message.Succeeded ||
                !sendSmsNotificationResult.Message.Succeed)
            {
                return await Task.FromResult(Result.Failure(sendSmsNotificationResult.Message.Errors));
            }

            await _dbContext.UserOTPs.AddAsync(new Domain.Entities.UserOTPs
            {
                UserId = user.Id,
                IsRecieved = true,
                OtpCode = hashedDigit,
                ExpireDate = DateTime.Now.AddMinutes(2),
                MessageBody = digit.ToString(),
                AccessToken = signinResponse.AccessToken,
                TokenExpireDate = signinResponse.ExpireDate,
                RefreshToken = signinResponse.RefreshToken,
                TokenType = signinResponse.TokenType,
                OtpCodeVerified = false
            });
            await _dbContext.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(Result.Success());
        }
        private async Task<SigninByOtpResponse> GetAccessToken(SigninByOtpCommand request)
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
                Scope = request.Scope
            });
            SigninByOtpResponse loginResponse = new()
            {
                AccessToken = requestPassword.AccessToken,
                TokenType = requestPassword.TokenType,
                ExpireDate = DateTime.Now.AddSeconds(requestPassword.ExpiresIn),
                RefreshToken = requestPassword.RefreshToken,
            };

            return await Task.FromResult(loginResponse);
        }
        public async Task<Result<List<Claim>>> GetUserClaimsAsync(ApplicationUser user, string? claimType)
        {
            IList<Claim> claims = await _signInManager.UserManager.GetClaimsAsync(user).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(claimType))
            {
                claims = claims.Where(x => x.Type == claimType).ToList();
            }
            return Result<List<Claim>>.Success(claims.ToList());
        }
    }
}
