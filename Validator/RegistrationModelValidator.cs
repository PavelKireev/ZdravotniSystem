using ZdravotniSystem.DB.Repository;
using ZdravotniSystem.Model;

namespace ZdravotniSystem.Validator
{
    public interface IRegistrationModelValidator
    {
        ValidationResult Validate(RegistrationModel model);
    }
    public class RegistrationModelValidator : IRegistrationModelValidator
    {
        private readonly IUserRepository _userRepository;
        private readonly ValidationResult _validationResult;

        public RegistrationModelValidator(
            IUserRepository userRepository
        ) {
            _userRepository = userRepository;
            _validationResult = new();
        }

        public ValidationResult Validate(RegistrationModel model)
        {
            _validateEmail(model);
            return _validationResult;
        }

        private void _validateEmail(RegistrationModel model)
        {
            if(string.IsNullOrEmpty(model.Email))
            {
                _validationResult.Message = "Invalid email format!";
                _validationResult.IsValid = false;
                return;
            }

            if (_userRepository.DoesEmailExist(model.Email))
            {
                _validationResult.Message = "Email already registered!";
                _validationResult.IsValid = false;
                return;
            }

            _validationResult.IsValid = true;
        }
    }
}
