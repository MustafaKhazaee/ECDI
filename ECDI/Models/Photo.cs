namespace ECDI.Models {
    public class Photo {
        public int Id { get; set; }
        public string URL { get; set; }
        public string? description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
