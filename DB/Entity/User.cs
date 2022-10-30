using Microsoft.AspNetCore.Identity;

namespace ZdravotniSystem.DB.Entity
{
    public abstract class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
