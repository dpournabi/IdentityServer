namespace NspForms.Core.Controllers;

[EnableCors("CorsPolicy")]
[ApiController]
[Route("api/[controller]/[action]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Heartbit() => await Task.FromResult(Ok(true));
}
