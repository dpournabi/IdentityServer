using DNTCaptcha.Core;
using IdentityServer.Application.Company.Queries;
using Microsoft.AspNetCore.RateLimiting;

namespace IdentityServer.Api.Controllers;

[Route("api/[controller]/[action]")]
public partial class AccountController : BaseApiController
{
    private readonly IDNTCaptchaApiProvider _apiProvider;
    public AccountController(IDNTCaptchaApiProvider apiProvider)
    {
        _apiProvider = apiProvider;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<Guid?>))]
    public async Task<IActionResult> Signup([FromBody] CreateSignupUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    [ValidateDNTCaptcha(ErrorMessage = "کد امنیتی را وارد کنید")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<SigninResponse>))]
    public async Task<IActionResult> Signin([FromForm] SigninCommand command)
    {

        if (!ModelState.IsValid)
        {
            return Ok(Result<SigninResponse>.Failure("کد کپچا صحیح نمی باشد"));
        }

        return Ok(await this.Mediator.Send(command));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<SigninByOtpResponse>))]
    public async Task<IActionResult> SigninByOtp([FromBody] SigninByOtpCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<IEnumerable<RoleClaim>>))]
    public async Task<IActionResult> GetMyClaims()
    {
        return Ok(await this.Mediator.Send(new GetMyClaimsQuery() { UserName = this.User.Claims.First(c => c.Type == "name").Value }));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<IEnumerable<MyRoleResponse>>))]
    public async Task<IActionResult> GetMyRoles()
    {
        return Ok(await this.Mediator.Send(new MyRolesQuery() { UserName = this.User.Claims.First(c => c.Type == "name").Value }));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<IEnumerable<GetCompanyResponse>>))]
    public async Task<IActionResult> GetCompanies()
    {
        return Ok(await this.Mediator.Send(new GetCompanyQuery() { }));
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<UserProfileResponse>))]
    public async Task<IActionResult> MyProfile()
    {
        return Ok(await this.Mediator.Send(new MyProfileQuery() { UserName = this.User.Claims.First(c => c.Type == "name").Value }));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<ActionResult<Result>> SignOut([FromBody] SignOutCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        if (!string.IsNullOrEmpty(command.UserName) &&
            (User.IsInRole(AssessorsManager.Administrator) || User.IsInRole(AssessorsManager.Root)))
        {
            //..........
        }
        else
        {
            command.UserName = this.User.Claims.First(c => c.Type == "name").Value;
        }
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<SigninByOtpResponse>))]
    public async Task<IActionResult> VerifyUserOtp([FromBody] VerifyUserOtpCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<List<RoleClaimResponse>>))]
    public async Task<IActionResult> GetRoleClaims(RoleClaimQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    [AllowAnonymous]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
    [EnableRateLimiting(DNTCaptchaRateLimiterPolicy.Name)]
    public ActionResult<DNTCaptchaApiResponse> CreateDNTCaptchaParams()
    {
        // Note: For security reasons, a JavaScript client shouldn't be able to provide these attributes directly.
        // Otherwise an attacker will be able to change them and make them easier!
        return _apiProvider.CreateDNTCaptcha(new DNTCaptchaTagHelperHtmlAttributes
        {
            BackColor = "#f7f3f3",
            FontName = "Tahoma",
            FontSize = 18,
            UseRelativeUrls = true,
            ForeColor = "#111111",
            Language = Language.Persian,
            DisplayMode = DisplayMode.ShowDigits,
            Max = 99999,
            Min = 10000,
        });
    }
}
