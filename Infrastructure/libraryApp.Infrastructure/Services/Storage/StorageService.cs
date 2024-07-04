using libraryApp.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }// orn LocalStorage

        public async Task DeleteAsync(string pathOrContainerName, string filename)
        =>await _storage.DeleteAsync(pathOrContainerName, filename);

        public List<string> GetFiles(string pathOrContainerName)
        =>_storage.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
        => _storage.HasFile(pathOrContainerName, fileName);

        public async Task<List<(string filename, string pathOrContainer)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        => await _storage.UploadAsync(pathOrContainerName, files);
    }
}
