using libraryApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Domain.Entities
{
    public class Book:BaseEntity
    {
        public string Title { get; set; } // baslıgı
        public string Author { get; set; } // yazar
        public string ISBN { get; set; } // isbn
        public string PositionOnShelf { get; set; }  // Raf üzerindeki konumu
        public bool IsAvailable { get; set; }// book başkasında olabilir
        public string ShelfId { get; set; }
        public Shelf Shelf { get; set; }

        public ICollection<Note> Notes { get; set; }
        public ICollection<BookHistory> History { get; set; }
        public ICollection<BookImageFile> BookImageFiles { get; set; }
    }
}
