using BlackTitanium.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlackTitanium.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("[controller]")]
public class AuthController(DatabaseContext db) : ControllerBase {
    [Inject]
    public DatabaseContext Db { get; private set; } = db;

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request) {
        return StatusCode(503);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request) {
        var user = await Db.Users.FirstOrDefaultAsync(x => x.Login == request.Login);
        if (user is null) {
            return BadRequest(new Error() {
                Message = "User not found"
            });
        }

        if (CheckPassword(user, request.Password)) {
            return BadRequest(new Error() {
                Code = 401,
                Message = "Wrong password"
            });
        }
        
        return Ok(new Object<User> {
            Content = user
        });
    }

    public IActionResult Index() {
        return Ok("Hello world");
    }

    private static bool CheckPassword(User user, string password) {
        return user.PasswordHash == Utils.Cryptography.Sha1(password + user.PasswordSalt);
    }
}