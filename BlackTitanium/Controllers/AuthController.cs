using Microsoft.AspNetCore.Mvc;

namespace BlackTitanium.Controllers; 

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase {
    [HttpPost]
    public IActionResult Register() {
        return Ok();
    }
}