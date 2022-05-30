namespace ECDI.Models {
    public class Teacher {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Introduction { get; set; }
        public string? Email { get ; set; }
        public string? Mobile1 { get; set; }
        public string? Mobile2 { get; set; }
        public byte[]? Photo { get; set; }
        public string? Facebook { get; set; }
    }
}
