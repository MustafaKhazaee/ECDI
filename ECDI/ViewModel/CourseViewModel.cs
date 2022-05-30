namespace ECDI.ViewModel {
    public class CourseViewModel {
        public string Name { set; get; }
        public int TeacherId { set; get; }
        public string? Description { set; get; }
        public DateTime? CreatedDate { set; get; } = DateTime.Now;
        public DateTime? StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        public string Type { get; set; }
        public string? Time { get; set; }
        public string? Length { get; set; }
        public string? Level { get; set; }
        public string? YouTubeURL { get; set; }
        public string? Price { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
