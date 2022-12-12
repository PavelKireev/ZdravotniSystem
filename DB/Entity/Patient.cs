namespace ZdravotniSystem.DB.Entity
{
    public class Patient : User
    {
        public string BirthDate { get; set; }
        public int InsuranceNumber { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Patient patient &&
                   Id == patient.Id &&
                   FirstName == patient.FirstName &&
                   LastName == patient.LastName &&
                   Email == patient.Email &&
                   PhoneNumber == patient.PhoneNumber &&
                   BirthDate == patient.BirthDate &&
                   InsuranceNumber == patient.InsuranceNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Email, PhoneNumber, BirthDate, InsuranceNumber);
        }

        public override string? ToString()
        {
            return String.Format("{0} {1}", FirstName, LastName);
        }

    }
}
