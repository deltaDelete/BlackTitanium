using System.Globalization;
using System.Security.Claims;
using BlackTitanium.Models;
using BlackTitanium.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    public async Task<ActionResult<Response>> Register([FromBody] RegisterRequest request) {
        string login = request.Login;
        string password = request.Password;

        if (Db.Users.Any(u => u.Login == login)) {
            return Conflict(new Error() {
                Message = "User with this login already exists",
                Code = StatusCodes.Status409Conflict
            });
        }

        string passwordSalt = Guid.NewGuid().ToString().Split('-')[0];
        string passwordHash = Cryptography.Sha1(password + passwordSalt);

        var user = new User() {
            Login = request.Login,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Birthday = request.Birthday,
            GenderId = request.GenderId
        };

        var addedUser = await Db.Users.AddAsync(user);
        await Db.SaveChangesAsync();

        return Ok(addedUser.Entity);
    }

    [HttpPost("login")]
    public async Task<ActionResult<Models.Response>> Login([FromBody] LoginRequest request) {
        var user = await Db.Users.FirstOrDefaultAsync(x => x.Login == request.Login);
        if (user is null) {
            return BadRequest(new Error() {
                Message = "User not found"
            });
        }

        if (!CheckPassword(user, request.Password)) {
            return BadRequest(new Error() {
                Code = 401,
                Message = "Wrong password"
            });
        }

        var claims = new List<Claim> {
            new(ClaimTypes.Name, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.Email, user.Login),
            new("BirthDay", user.Birthday.ToString(DateTimeFormatInfo.InvariantInfo)),
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Gender, user.GenderId.ToString())
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity)
        );
        return Ok(new Object<User> {
            Content = user
        });
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }

    private static bool CheckPassword(User user, string password) {
        return user.PasswordHash == Utils.Cryptography.Sha1(password + user.PasswordSalt);
    }
}