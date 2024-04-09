using Learning.Dto;
using Learning.Models;
using Learning.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly IGenericService<Lesson, LessonDto> _lessonService;

        public LessonsController(IGenericService<Lesson, LessonDto> lessonService)
        {
            _lessonService = lessonService;
        }

        // GET: api/lessons
        [HttpGet]
        public async Task<IActionResult> GetAllLessons()
        {
            var lessons = await _lessonService.GetAllAsync();

            if(lessons is null)
                return NotFound();

            return Ok(lessons);
        }

        // GET: api/lessons/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLessonById(Guid id)
        {
            var lesson = await _lessonService.GetByIdAsync(id);

            if (lesson == null)
                return NotFound();

            return Ok(lesson);
        }

        // POST: api/lessons
        [HttpPost]
        public async Task<ActionResult<LessonDto>> Create([FromForm] LessonDto dto)
        {
            
            //Path.GetExtension(dto.Image.FileName).ToLower();

            //using var dataStream = new MemoryStream();

            //await dto.Image.CopyToAsync(dataStream);

            Lesson lesson = new Lesson
            {
                Id = new Guid(),
                LessonName = dto.LessonName,
                Description = dto.Description,
                ImageUrl = FileSetting.UploadFile(dto.Image)
            };

            await _lessonService.AddAsync(lesson);

            return Ok(lesson);
        }

        // PUT: api/lessons/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, LessonDto dto)
        {
            var lesson = await _lessonService.GetByIdAsync(id);

            if (lesson == null)
                return NotFound();

            await _lessonService.UpdateAsync(id, lesson , dto);

            return Ok("Updated successfully");
        }

        // DELETE: api/lessons/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _lessonService.DeleteAsync(id);

            return Ok("Removed successfully");
        }
    }
}
