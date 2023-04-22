namespace ZdravotniSystem.DTO
{
    public class AuthUserDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public int InsuranceNumber { get; set; }
        public int OfficeNumber { get; set; }
    }
}
