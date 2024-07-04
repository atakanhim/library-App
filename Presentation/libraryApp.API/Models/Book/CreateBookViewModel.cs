using libraryApp.Domain.Entities;

namespace libraryApp.API.Models.Book
{
    public class CreateBookViewModel
    {
        public string Id  = Guid.NewGuid().ToString();
        public string Title { get; set; } 
        public string Author { get; set; } 
        public string ImageUrl { get; set; }
        public string ISBN { get; set; }      
        public string PositionOnShelf { get; set; }  
        public bool IsAvailable { get; set; } = true;
        public string ShelfId { get; set; }
    }
}
