using Humanizer;
using Learning.Dto;
using Learning.Hubs;
using Learning.Models;
using Learning.Services;
using Learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LessonMaterialsController(IGenericService<Material, MaterialDto> materialService, IHubContext<LessonHub> lessonHubContext) : ControllerBase
    {
        private readonly IGenericService<Material, MaterialDto> _materialService= materialService;
        private readonly IHubContext<LessonHub> _LessonHubContext = lessonHubContext;


        // GET: api/lessonmaterials
        [HttpGet]
        public async Task<IActionResult> GetAllLessonMaterials()
        {
            var materials = await _materialService.GetAllAsync();

            if (materials is null)
                return NotFound();

            return Ok(materials);
        }

        // GET: api/lessonmaterials/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetLessonMaterialById(Guid id)
        {
            var material = await _materialService.GetByIdAsync(id);

            if (material == null)
                return NotFound();

            return Ok(material);
        }

        // POST: api/lessonmaterials
        [HttpPost]
        public async Task<ActionResult<Material>> Create(MaterialDto materialDto)
        {
            //convert from Dto to model
            var materialLesson = new Material
            {
                Id=new Guid(),
                Title=materialDto.Title,
                FileType= Path.GetExtension(materialDto.Content.FileName).ToLower(),
                ContentUrl= FileSetting.UploadFile(materialDto.Content),//should be edit in fileSetting
                LessonId=materialDto.LessonId,
            };
             
            await _materialService.AddAsync(materialLesson);
            
            return Ok(materialLesson);
        }

        // PUT: api/lessonmaterials/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,MaterialDto dto)
        {
            var material = await _materialService.GetByIdAsync(id);

            if (material is null)
            {
                return NotFound();
            }

            await _materialService.UpdateAsync(id, material,dto);

            await _LessonHubContext.Clients.All.SendAsync("LessonMaterialUpdated", id);

            return Ok("Updated successfully");
        }

        // DELETE: api/lessonmaterials/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _materialService.DeleteAsync(id);

            return Ok("Removed successfully");
        }
    }
}
