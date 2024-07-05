using libraryApp.Application.DTOs.NoteDTOs;

namespace libraryApp.API.Models.Shelf
{
    public class CreateShelfViewModel
    {
        public string Id = Guid.NewGuid().ToString();
        public string Name { get; set; }
    
    }
}
