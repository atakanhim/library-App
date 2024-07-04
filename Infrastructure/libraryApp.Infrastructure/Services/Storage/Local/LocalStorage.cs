using libraryApp.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Infrastructure.Services.Storage.Local
{
    public class LocalStorage :  Storage, ILocalStorage
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        public async Task DeleteAsync(string path, string filename)
         =>   File.Delete($"{path}\\{filename}");

        public List<string> GetFiles(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
           return dir.GetFiles().Select(f=>f.Name).ToList();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
           var model= File.Exists($"{pathOrContainerName}\\{fileName}");
            return model;
        }

        public async Task<List<(string filename, string pathOrContainer)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);


            List<(string filename, string path)> datas = new();
            foreach (IFormFile file in files)
            {
                string newname = await FileRenameAsync(uploadPath, file.FileName, HasFile);


                string fullpath = Path.Combine(uploadPath, newname);
                bool result = await CopyFileAsync(fullpath, file);
                datas.Add((newname, path+"/"+newname));
               
            }

          
            //todo egerki if gecerli degilse dosyaların sunucuda yüklenirken hata alındıgına dair exeption olsuturulup dondurulecek

            return datas;
        }
        async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                // using dememizin sebebi fonksiyon bittiginde dispose edilmek istenmesi ,await dedik cunku asyncidspose tan türemiş
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream); ;
                await fileStream.FlushAsync();// filestream ile yaptıgımız calismalari kaldiriyoruz
                return true;
            }
            catch (Exception ex)
            {
                //todo log
                throw ex;
            }
        }

    }
}
