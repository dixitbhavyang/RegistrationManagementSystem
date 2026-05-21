using Microsoft.AspNetCore.Http;
using RegistrationManagementSystem.API.Data;
using RegistrationManagementSystem.API.Models;
using Microsoft.EntityFrameworkCore;

namespace RegistrationManagementSystem.API.Helpers
{
    public class FileHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;

        public FileHelper(IConfiguration configuration, IWebHostEnvironment environment, AppDbContext context)
        {
            _configuration = configuration;
            _environment = environment;
            _context = context;
        }

        public async Task<List<UserDocument>> SaveFilesAsync(List<IFormFile> files, int registrationId)
        {
            var documents = new List<UserDocument>();
            var fileSettings = _configuration.GetSection("FileSettings");
            var maxFileSizeInMB = int.Parse(fileSettings["MaxFileSizeInMB"]!);
            var allowedExtensions = fileSettings.GetSection("AllowedExtensions").Get<List<string>>()!;

            var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads", registrationId.ToString());
            Directory.CreateDirectory(uploadsFolder);

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    continue;

                if (file.Length > maxFileSizeInMB * 1024 * 1024)
                    continue;

                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var document = new UserDocument
                {
                    RegistrationId = registrationId,
                    FileName = file.FileName,
                    FilePath = filePath,
                    FileSize = file.Length,
                    ContentType = file.ContentType
                };

                _context.Documents.Add(document);
                documents.Add(document);
            }

            await _context.SaveChangesAsync();
            return documents;
        }

        public async Task<(byte[] FileBytes, string ContentType, string FileName)?> GetFileAsync(int documentId)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document == null || !File.Exists(document.FilePath))
                return null;

            var fileBytes = await File.ReadAllBytesAsync(document.FilePath);
            return (fileBytes, document.ContentType, document.FileName);
        }
    }
}