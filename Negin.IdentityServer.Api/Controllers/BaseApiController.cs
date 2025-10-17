namespace IdentityServer.Api.Controllers;

[ApiController]
//[DisableCors]
[EnableCors("CorsPolicy")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class BaseApiController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator == null ? HttpContext.RequestServices.GetRequiredService<ISender>() : _mediator;
}
