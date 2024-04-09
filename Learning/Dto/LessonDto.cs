using System.ComponentModel.DataAnnotations;

namespace Learning.Dto
{
    public class LessonDto
    {
        [Required]
        public string LessonName { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Image { get; set; } 

    }
}
