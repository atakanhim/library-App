using libraryApp.Application.DTOs.NoteDTOs;

namespace libraryApp.API.Models.Note
{
    public class UpdateNoteViewModel
    {
        public string Id  { get;set; }
        public string? Content { get; set; }
        public PrivacySettingEnum? Privacy { get; set; } = null;// Gizlilik ayarı
    }
}
