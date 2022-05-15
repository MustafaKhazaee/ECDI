namespace ECDI.Models {
    public class Course {
        public int Id { set; get; }
        public string Name { set; get; }
        public int TeacherId { set; get; }
        public Teacher Teacher { get; set; }
        public string? Description { set; get; }
        public DateTime? CreatedDate { set; get; } = DateTime.Now;
        public DateTime? StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        public string? Photo { set; get; }
        public string? Time { get; set; }
    }
}
