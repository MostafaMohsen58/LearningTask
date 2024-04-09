using Humanizer;
using Learning.Data;
using Learning.Dto;
using Learning.Models;
using Learning.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Learning.Services
{
    public class LessonService: IGenericService<Lesson, LessonDto>
    {
        private readonly ApplicationDbContext _context;

        public LessonService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task AddAsync(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
        }
        
        public async Task<Lesson> GetByIdAsync(Guid id)
        {
            return await _context.Lessons.Include(l => l.Materials).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<Lesson>> GetAllAsync()
        {
            return await _context.Lessons.Include(l => l.Materials).ToListAsync();
        }
        
        public async Task UpdateAsync(Guid id, Lesson existingLesson, LessonDto lesson)
        {
            //var existingLesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);

            if (existingLesson != null)
            {
                existingLesson.LessonName = lesson.LessonName;
                existingLesson.Description = lesson.Description;
                if (lesson.Image is not null)
                {
                    //to do :delete old picture from server
                    FileSetting.DeleteFile(existingLesson.ImageUrl, FileSetting.LocationFile(existingLesson.ImageUrl));

                    existingLesson.ImageUrl = FileSetting.UploadFile(lesson.Image);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);

            if (lesson != null)
            {
                //to do :delete the picture from server
                FileSetting.DeleteFile(lesson.ImageUrl, FileSetting.LocationFile(lesson.ImageUrl));

                _context.Lessons.Remove(lesson);
                await _context.SaveChangesAsync();
            }
        }
        
    }
}
