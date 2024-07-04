using libraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.DTOs.Shelf
{
    public class BaseShelfDTO
    {
        public string Name { get; set; } // rafın ismi
        //public ICollection<Book> Books { get; set; } // raftaki kitaplar
    }
}
