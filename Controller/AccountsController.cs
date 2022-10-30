using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using ZdravotniSystem.Configuration.Security;
using ZdravotniSystem.Configuration.Security.dto;
using ZdravotniSystem.DB.Entity;

namespace ZdravotniSystem.Controller
{
    [ApiController]
    public class AccountsController
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtHandler _jwtHandler;

        public AccountsController(
            UserManager<User> userManager,
            JwtHandler jwtHandler
        ) {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }
    }
}
