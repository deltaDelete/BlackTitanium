using BlackTitanium.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlackTitanium.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("[controller]")]
public class AuthController : ControllerBase {
    [Inject] 
    public DatabaseContext Db { get; set; } = null!;

    [HttpPost]
    public IActionResult Register() {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromQuery] string username, [FromQuery] string password) {
        var count = Db.Genders.Count();
        if (count == 2) {
            return Ok("БАЗА");
        }

        // todo password hash
        var user = Db.Users.FirstOrDefault(x => x.Login == username && x.PasswordHash == password);
        if (user is not null) {
            return Ok(new Object<User> {
                Content = user
            });
        }
        return BadRequest(new Error() {
            Message = "User not found"
        });
    }
}