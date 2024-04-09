namespace Learning.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public string LessonName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public ICollection<Material> Materials { get; set; } = new HashSet<Material>();
    }
}
