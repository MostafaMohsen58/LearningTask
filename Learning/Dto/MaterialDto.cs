namespace Learning.Dto
{
    public class MaterialDto
    {
        public string Title { get; set; }
        public IFormFile? Content { get; set; } /*= default!;*///video for example
        public Guid? LessonId { get; set; }

    }
}
