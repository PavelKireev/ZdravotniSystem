using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Repository;
using ZdravotniSystem.DTO;
using ZdravotniSystem.Model;
using ZdravotniSystem.Service;
using ZdravotniSystem.Validator;

namespace ZdravotniSystem.Controller
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly ILogger<AuthenticationController> _logger;

        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IPatientService _patientService;

        private readonly IRegistrationModelValidator _registrationModelValidator;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IAuthService authService,
            IConfiguration configuration, 
            IUserRepository userRepository, 
            IPatientService patientService, 
            IRegistrationModelValidator registrationModelValidator
        ) {
            _logger = logger;
            _authService = authService;
            _configuration = configuration;
            _userRepository = userRepository;
            _patientService = patientService;
            _registrationModelValidator = registrationModelValidator;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model is null)
            {
                return BadRequest("Invalid user request!!!");
            }

            User user = _userRepository.GetOneByEmail(model.UserName);

            if (user != null && model.UserName.Equals(user.Email) && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                var tokenClaims = new[] {
                    new Claim("id", user.Id.ToString()),
                    new Claim("email", user.Email),
                    new Claim("role", user.Role)
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
        public IActionResult SignUp([FromBody] RegistrationModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            ValidationResult validationResult = _registrationModelValidator.Validate(model);

            if (validationResult.IsValid)
            {
                _patientService.RegisterPatient(model);
                return Ok();
            }

            return BadRequest(validationResult.Message);
        }

        [HttpGet("my-profile"), Authorize]
        public AuthUserDto GetMyProfile()
        {
            return _authService.GetAuthenticatedUser(
                            this.User.Claims.First(i => i.Type.Equals(ClaimTypes.Email)).Value,
                            this.User.Claims.First(i => i.Type.Equals(ClaimTypes.Role)).Value
                            );

        }
    }
}
