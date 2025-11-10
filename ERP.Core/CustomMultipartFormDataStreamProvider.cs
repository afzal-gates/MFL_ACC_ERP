using System.IO;
using Microsoft.AspNetCore.Http;

namespace ERP.Core
{
    // Note: ASP.NET Core uses IFormFile for file uploads instead of MultipartFormDataStreamProvider
    // This class provides helper methods for file handling
    public class CustomMultipartFormDataStreamProvider
    {
        public string RootPath { get; set; }

        public CustomMultipartFormDataStreamProvider(string path)
        {
            RootPath = path;
        }

        /// <summary>
        /// Gets a unique file name by appending (1), (2), etc. if file exists
        /// </summary>
        public string GetUniqueFileName(string fileName)
        {
            int count = 1;
            string fileNameOnly = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);

            string fullPath = Path.Combine(RootPath, fileName);
            string newFullPath = fullPath;

            string localFileName = fileName;
            while (File.Exists(newFullPath))
            {
                localFileName = string.Format("{0}({1}){2}", fileNameOnly, count++, extension);
                newFullPath = Path.Combine(RootPath, localFileName);
            }
            return localFileName;
        }

        /// <summary>
        /// Saves an IFormFile to disk with a unique file name
        /// </summary>
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is null or empty");

            // Get unique file name
            string fileName = GetUniqueFileName(file.FileName);
            string fullPath = Path.Combine(RootPath, fileName);

            // Ensure directory exists
            Directory.CreateDirectory(RootPath);

            // Save file
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
