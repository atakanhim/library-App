using libraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.DTOs.Book
{
    public class CreateBookDTO:BaseDTO
    {
        public string Title { get; set; } // baslıgı
        public string Author { get; set; } // yazar
        public string ImageUrl { get; set; }
        public string ISBN { get; set; } // isbn
        public string PositionOnShelf { get; set; }  // Raf üzerindeki konumu
        public bool IsAvailable { get; set; }// book başkasında olabilir
        public string ShelfId { get; set; }
    }
}
