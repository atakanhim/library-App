using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.Abstractions.Storage
{
    public interface IStorage
    {
        Task<List<(string filename, string pathOrContainer)>> UploadAsync(string pathOrContainerName, IFormFileCollection files);// tuple nesne donduruyoruz

        Task DeleteAsync(string pathOrContainerName, string filename);
        List<string> GetFiles(string pathOrContainerName);

        bool HasFile(string pathOrContainerName,string fileName);
    }
}
