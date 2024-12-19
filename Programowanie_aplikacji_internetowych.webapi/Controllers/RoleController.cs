using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Programowanie_aplikacji_internetowych.application.Interfaces.Services;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Repository;

namespace Programowanie_aplikacji_internetowych.webapi.Controllers;
[ApiController]
[Route("api/roles")]
public class RoleController : Controller
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleService.GetRoles();
        return Ok(roles);
    }
}
