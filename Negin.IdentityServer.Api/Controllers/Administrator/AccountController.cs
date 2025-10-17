using IdentityServer.Application.Company.Commands.AddCompeny;
using IdentityServer.Application.Company.Commands.EditCompany;
using IdentityServer.Application.Company.Commands.RemoveCompeny;
using IdentityServer.Application.User.Queries.AllUsers;

namespace IdentityServer.Api.Controllers;

public partial class AccountController : BaseApiController
{
    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPost, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<List<string>>))]
    public async Task<IActionResult> GetUserRoles(UserRoleQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    //[ClaimRequirementFilter(ClaimValueCheck = "FormKey10")]
    [HttpGet, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<IEnumerable<GetAllRolesResponse>>))]
    public async Task<IActionResult> GetAllRoles([FromQuery] GetAllRolesQuery query)
    {
        return Ok(await this.Mediator.Send(query));
    }
    [Route("/api/[controller]/Administrator/[action]")]
    [HttpDelete, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<IEnumerable<bool>>))]
    public async Task<IActionResult> DeleteCompanies([FromQuery] DeleteCompanyCommand body)
    {
        return Ok(await this.Mediator.Send(body));
    }
    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPost, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<IEnumerable<bool>>))]
    public async Task<IActionResult> AddCompanie([FromBody] AddCompanyCommand body)
    {
        return Ok(await this.Mediator.Send(body));
    }
    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPut, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<IEnumerable<bool>>))]
    public async Task<IActionResult> EditCompanie([FromBody] EditCompanyCommand body)
    {
        return Ok(await this.Mediator.Send(body));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpGet, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<IEnumerable<GetAllUsersResponse>>))]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQuery query)
    {
        return Ok(await this.Mediator.Send(query));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPut, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<IActionResult> UnlockUser([FromBody] UnlockUserCommand query)
    {
        return Ok(await this.Mediator.Send(query));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpDelete, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<bool>))]
    public async Task<IActionResult> DeleteRole([FromQuery] RemoveRoleCommand command)
    {
        return Ok(await this.Mediator.Send(command));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPost, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<bool>))]
    public async Task<IActionResult> DeleteUser([FromBody] RemoveUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPost, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateProfileCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPost, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<IActionResult> AssignUserToRole([FromBody] AssignUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPost, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<bool>))]
    public async Task<IActionResult> AddRole([FromBody] AddRoleCommand command)
    {
        return Ok(await this.Mediator.Send(command));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPost, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<bool>))]
    public async Task<IActionResult> EditRole([FromBody] EditRoleCommand command)
    {
        return Ok(await this.Mediator.Send(command));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpGet, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<IEnumerable<GetRoleGroupResponse>>))]
    public async Task<IActionResult> GetGroups([FromQuery] GetGroupsQuery query)
    {
        return Ok(await this.Mediator.Send(query));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPost, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<bool>))]
    public async Task<IActionResult> AddRoleGroups([FromBody] AddRoleGroupsCommand command)
    {
        return Ok(await this.Mediator.Send(command));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpGet, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<IEnumerable<ClientClaims>>))]
    public async Task<IActionResult> GetAllClaims()
    {
        return Ok(await this.Mediator.Send(new GetAllClaimsQuery()));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpGet, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<IEnumerable<GetRoleClaimsResponse>>))]
    public async Task<IActionResult> GetRoleClaims([FromQuery] GetRoleClaimsQuery query)
    {
        return Ok(await this.Mediator.Send(query));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPost, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<bool>))]
    public async Task<IActionResult> AddRoleClaims([FromBody] AddRoleClaimCommand command)
    {
        return Ok(await this.Mediator.Send(command));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPost, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result<bool>))]
    public async Task<IActionResult> SaveUserRoles([FromBody] SaveUserRolesCommand command)
    {
        return Ok(await this.Mediator.Send(command));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPost, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<IActionResult> VerifyClientByAdmin([FromQuery] Guid userId)
    {
        return Ok(await this.Mediator.Send(new VerifyClientCommand() { UserId = userId, Verify = true }));
    }

    [Route("/api/[controller]/Administrator/[action]")]
    [HttpPost, Authorize(Roles = AssessorsManager.Root + "," + AssessorsManager.Administrator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType(typeof(Result))]
    public async Task<IActionResult> DeactivateUser([FromQuery] Guid userId)
    {
        return Ok(await this.Mediator.Send(new VerifyClientCommand() { UserId = userId, Verify = false }));
    }

}
