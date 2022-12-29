using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Repository;
using ZdravotniSystem.Model;
using ZdravotniSystem.Service;

namespace ZdravotniSystem.Controller
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IPatientService _patientService;

        public AuthenticationController(
            IConfiguration configuration,
            IUserRepository userRepository,
            IPatientService patientService
        ) {
            _configuration = configuration;
            _userRepository = userRepository;
            _patientService = patientService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model is null)
            {
                return BadRequest("Invalid user request!!!");
            }

            User user = _userRepository.GetOneByEmail(model.UserName);


            if (model.UserName.Equals(user.Email) && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                var tokenClaims = new[] {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "PATIENT")
                }; 

                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: jwtSettings["validIssuer"],
                    audience: jwtSettings["validAudience"],
                    claims: tokenClaims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new JwtTokenResponseDto { Token = tokenString });
            }
            return Unauthorized();
        }

        [HttpPost("sign-up")]
        public IActionResult SignUp([FromBody] RegistrationModel model) {
            if (model != null)
            {
                patientService.registerPatient(model);
                return Ok();
            }

            return BadRequest();
            
        }
    }
}
