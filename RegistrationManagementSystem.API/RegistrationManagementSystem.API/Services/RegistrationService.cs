using RegistrationManagementSystem.API.DTOs;
using RegistrationManagementSystem.API.Helpers;
using RegistrationManagementSystem.API.Models;
using RegistrationManagementSystem.API.Repositories;
using RegistrationManagementSystem.API.Repositories.Interfaces;
using RegistrationManagementSystem.API.Services.Interfaces;

namespace RegistrationManagementSystem.API.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly FileHelper _fileHelper;

        public RegistrationService(IRegistrationRepository registrationRepository, FileHelper fileHelper)
        {
            _registrationRepository = registrationRepository;
            _fileHelper = fileHelper;
        }

        public async Task<bool> CreateAsync(RegisterDTO dto, int userId, List<IFormFile> files)
        {
            var registration = new Registration
            {
                UserId = userId,
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Hobbies = string.Join(",", dto.Hobbies),
                Address = dto.Address,
                StateId = dto.StateId,
                CityId = dto.CityId,
                Pincode = dto.Pincode
            };

            var savedRegistration = await _registrationRepository.CreateAsync(registration);

            if (files != null && files.Count > 0)
            {
                var documents = await _fileHelper.SaveFilesAsync(files, savedRegistration.Id);
                savedRegistration.Documents = documents;
                await _registrationRepository.UpdateAsync(savedRegistration);
            }

            return true;
        }

        public async Task<(List<RegistrationListDTO> Items, int TotalCount)> GetAllAsync(int page, int pageSize, string? sortBy, string? filterByName)
        {
            var (items, totalCount) = await _registrationRepository.GetAllAsync(page, pageSize, sortBy, filterByName);

            var dtos = items.Select(r => new RegistrationListDTO
            {
                Id = r.Id,
                Name = r.Name,
                Username = r.User.Username,
                Gender = r.Gender,
                Hobbies = r.Hobbies,
                Address = r.Address,
                State = r.State.Name,
                City = r.City.Name,
                Pincode = r.Pincode,
                DateOfBirth = r.DateOfBirth,
                CreatedAt = r.CreatedAt,
                Documents = r.Documents.Select(d => new DocumentDTO
                {
                    Id = d.Id,
                    FileName = d.FileName,
                    ContentType = d.ContentType,
                    FileSize = d.FileSize
                }).ToList()
            }).ToList();

            return (dtos, totalCount);
        }

        public async Task<RegistrationListDTO?> GetByIdAsync(int id)
        {
            var r = await _registrationRepository.GetByIdAsync(id);
            if (r == null) return null;

            return new RegistrationListDTO
            {
                Id = r.Id,
                Name = r.Name,
                Username = r.User.Username,
                Gender = r.Gender,
                Hobbies = r.Hobbies,
                Address = r.Address,
                StateId = r.StateId,
                State = r.State.Name,
                CityId = r.CityId,
                City = r.City.Name,
                Pincode = r.Pincode,
                DateOfBirth = r.DateOfBirth,
                CreatedAt = r.CreatedAt,
                Documents = r.Documents.Select(d => new DocumentDTO
                {
                    Id = d.Id,
                    FileName = d.FileName,
                    ContentType = d.ContentType,
                    FileSize = d.FileSize
                }).ToList()
            };
        }

        public async Task<bool> UpdateAsync(int id, RegisterDTO dto, int userId)
        {
            var registration = await _registrationRepository.GetByIdAsync(id);
            if (registration == null) return false;

            registration.Name = dto.Name;
            registration.DateOfBirth = dto.DateOfBirth;
            registration.Gender = dto.Gender;
            registration.Hobbies = string.Join(",", dto.Hobbies);
            registration.Address = dto.Address;
            registration.StateId = dto.StateId;
            registration.CityId = dto.CityId;
            registration.Pincode = dto.Pincode;

            await _registrationRepository.UpdateAsync(registration);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _registrationRepository.DeleteAsync(id);
        }

        public async Task<(byte[] FileBytes, string ContentType, string FileName)?> DownloadDocumentAsync(int documentId)
        {
            return await _fileHelper.GetFileAsync(documentId);
        }
    }
}