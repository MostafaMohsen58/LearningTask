namespace Learning.Models
{
    public class Material
    {
        public Guid Id { get; set; }
        //public string VideoUrl { get; set; } = default!;
        //public string PdfFileName { get; set; } = default!;
        public string Title { get; set; }
        public string FileType { get; set; } // e.g., "video", "pdf", etc.
        public string ContentUrl { get; set; } //video for example

        public Guid? LessonId { get; set; }
        public Lesson Lesson { get; set; }

    }
}
