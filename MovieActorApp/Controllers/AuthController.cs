
using Microsoft.AspNetCore.Mvc;
using MovieActorApp.Application.Interfaces;
using System.Collections.Generic;

namespace MovieActorApp.Controllers
{
        [ApiController]
        [Route("api/auth")]
        public class AuthController : ControllerBase
        {
            private readonly IAuthService _authService;

            public AuthController(IAuthService authService)
            {
                _authService = authService;
            }

            [HttpPost("login")]
            public IActionResult Login([FromBody] LoginRequest request)
            {
                if (request.Username == "admin" && request.Password == "admin123")
                {
                    var token = _authService.GenerateJwtToken(request.Username, "Admin");
                    return Ok(new { Token = token });
                }
                else
                {
                    // Generate token for 'guest' if login fails
                    var token = _authService.GenerateJwtToken("guest", "Guest");
                    return Ok(new { Token = token });
                }
            return Unauthorized(new { Message = "Invalid credentials" });
            }
            [HttpGet("test-claims")]
            public IActionResult TestClaims()
            {
                var claims = User.Claims.Select(c => new { c.Type, c.Value });
                return Ok(claims);
            }

    }

    public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    

}
