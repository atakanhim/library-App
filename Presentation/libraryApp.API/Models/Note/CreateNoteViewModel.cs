using libraryApp.Application.DTOs.NoteDTOs;
using libraryApp.Domain.Entities;

namespace libraryApp.API.Models.Note
{
    public class CreateNoteViewModel
    {
        public string Id = Guid.NewGuid().ToString();
        public string Content { get; set; }
        public PrivacySettingEnum Privacy { get; set; } // Gizlilik ayarı
        public string BookId { get; set; }
        public string UserId { get; set; }
    }
}
