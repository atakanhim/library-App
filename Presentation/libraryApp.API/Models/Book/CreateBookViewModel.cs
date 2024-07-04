using libraryApp.Domain.Entities;

namespace libraryApp.API.Models.Book
{
    public class CreateBookViewModel
    {
        public string Id  = Guid.NewGuid().ToString();
        public string Title { get; set; } // baslıgı
        public string Author { get; set; } // yazar
        public string ISBN { get; set; } // isbn
        public string PositionOnShelf { get; set; }  // Raf üzerindeki konumu
        public bool IsAvailable { get; set; } = true;// book başkasında olabilir
        public string ShelfId { get; set; }
    }
}
