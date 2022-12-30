namespace ZdravotniSystem.DB.Entity
{
    public class UserToken : BaseEntity
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? ExparationTime { get; set; }
    }
}
