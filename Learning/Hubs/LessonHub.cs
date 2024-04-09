using Microsoft.AspNetCore.SignalR;

namespace Learning.Hubs
{
    public class LessonHub:Hub
    {
        public async Task UpdateLesson(Guid lessonId)
        {
            await Clients.All.SendAsync("LessonUpdated", lessonId);
        }

        public async Task UpdateLessonMaterial(Guid materialId)
        {
            await Clients.All.SendAsync("LessonMaterialUpdated", materialId);
        }
    }
}
