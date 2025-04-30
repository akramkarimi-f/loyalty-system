using LoyaltySystem.Application.Users;
using LoyaltySystem.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LoyaltySystem.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Earn points for a specific user.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="request">The request containing the number of points to earn.</param>
    /// <returns>A success message if points are earned successfully.</returns>
    /// <response code="200">Points earned successfully.</response>
    /// <response code="401">Unauthorized access.</response>
    /// <response code="400">Invalid request data.</response>
    [Authorize]
    [HttpPost("{id}/earn")]
    [SwaggerOperation(Summary = "Earn points for a user", Description = "Allows a user to earn loyalty points.")]
    [SwaggerResponse(200, "Points earned successfully.")]
    [SwaggerResponse(401, "Unauthorized access.")]
    [SwaggerResponse(400, "Invalid request data.")]
    public async Task<IActionResult> EarnPoints(int id, [FromBody] EarnPointsRequest request)
    {
        await _userService.EarnPointsAsync(id, request.Points);
        return Ok(new { Message = "Points earned successfully." });
    }
}
