namespace tipJar.Models
{
    public class File
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string BlobUrl { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
