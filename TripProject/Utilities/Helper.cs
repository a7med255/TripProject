using static System.Net.Mime.MediaTypeNames;

namespace TripProject.Utilities
{
    public static class Helper
    {
        public static async Task<string> UploadImage(List<IFormFile> Files,string folderName)//to save imge in project
        {
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".jpg";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads/"+ folderName, ImageName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await file.CopyToAsync(stream);
                        return ImageName;
                    }
                }
            }
            return string.Empty;
        }
    }
}
