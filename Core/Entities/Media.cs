namespace Core.Entities
{
    public class MediaFile : EntityBase
    {
        public string FileName { get; set; }
        public string Url { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public MediaFileType MediaFileType { get; set; }
    }
}
