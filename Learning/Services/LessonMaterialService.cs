using Learning.Data;
using Learning.Dto;
using Learning.Models;
using Learning.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Learning.Services
{
    public class LessonMaterialService:IGenericService<Material,MaterialDto>
    {
        private readonly ApplicationDbContext _context;

        public LessonMaterialService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        
        public async Task AddAsync(Material material)
        {
            _context.LessonMaterials.Add(material);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Material>> GetAllAsync()
        {
            return await _context.LessonMaterials.ToListAsync();
        }

        public async Task<Material> GetByIdAsync(Guid id)
        {
            return await _context.LessonMaterials.FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task UpdateAsync(Guid id, Material existingMaterial, MaterialDto dto)
        {
            existingMaterial.Title = dto.Title;
            if (dto.Content is not null)
            {
                FileSetting.DeleteFile(existingMaterial.ContentUrl, FileSetting.LocationFile(existingMaterial.ContentUrl));
                existingMaterial.ContentUrl = FileSetting.UploadFile(dto.Content);
                existingMaterial.FileType = Path.GetExtension(existingMaterial.ContentUrl).ToLower();
            }
            existingMaterial.LessonId = dto.LessonId;
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var material = await _context.LessonMaterials.FirstOrDefaultAsync(m => m.Id == id);
            if (material != null)
            {
                //delete content from server
                FileSetting.DeleteFile(material.ContentUrl, FileSetting.LocationFile(material.ContentUrl));

                _context.LessonMaterials.Remove(material);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}
