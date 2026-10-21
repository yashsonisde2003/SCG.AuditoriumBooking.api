using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [Authorize]
    [HttpGet("secure")]
    public IActionResult Secure()
    {
        return Ok(new
        {
            message = "You are authenticated!",
            userId = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult AdminOnly()
    {
        return Ok(new { message = "Admin access granted." });
    }
}