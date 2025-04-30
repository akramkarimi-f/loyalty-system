using LoyaltySystem.WebApi.Application.Services;
using LoyaltySystem.WebApi.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltySystem.WebApi.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpPost("{id}/earn")]
    public async Task<IActionResult> EarnPoints(int id, [FromBody] EarnPointsRequest request)
    {
        await _userService.EarnPointsAsync(id, request.Points);
        return Ok(new { Message = "Points earned successfully." });
    }
}