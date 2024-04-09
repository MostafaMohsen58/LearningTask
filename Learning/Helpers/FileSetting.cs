using Learning.Dto;

namespace Learning
{
    public static class FileSetting
    {
        public static string UploadFile(IFormFile file)
        {
            string location = LocationFile(file.FileName);

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{location}");

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            string filePath = Path.Combine(folderPath, fileName);

            using var fs = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fs);
            return fileName;
        }
        public static void DeleteFile(string fileName,string location)
        {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{location}");

                string filePath = Path.Combine(folderPath, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
        }
        public static string LocationFile(string fileName)
        {
            string Extension = Path.GetExtension(fileName).ToLower();

            string location = "";
            if (Extension == ".jpeg" || Extension == ".png")
                location = "Images";
            else if (Extension == ".mp4")
                location = "Videos";
            else
                location = "Documents";

            return location;
        }
    }
}
