using Microsoft.AspNetCore.SignalR;

namespace Learning.Hubs
{
    public class LessonHub:Hub
    {
        public async Task UpdateLesson(int lessonId)
        {
            await Clients.All.SendAsync("LessonUpdated", lessonId);
        }

        public async Task UpdateLessonMaterial(int materialId)
        {
            await Clients.All.SendAsync("LessonMaterialUpdated", materialId);
        }
    }
}
