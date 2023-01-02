using System.Runtime.ConstrainedExecution;

namespace ZdravotniSystem.Model
{
    public class RegistrationModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? InsuranceNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
