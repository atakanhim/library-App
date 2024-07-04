using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.DTOs.Book
{
    public class UploadBookImageDto
    {
        public string Id { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
