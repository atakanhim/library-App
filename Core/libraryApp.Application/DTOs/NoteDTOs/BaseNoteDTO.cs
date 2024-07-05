using libraryApp.Domain.Entities;
using libraryApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.DTOs.NoteDTOs
{
    public class BaseNoteDTO:BaseDTO
    {
        public string Content { get; set; }
        public PrivacySettingEnum? Privacy { get; set; } = null; // Gizlilik ayarı
 
    }
    public enum PrivacySettingEnum
    {
        IsPublic,
        IsFriendsOnly,
        IsPrivate
    }
}
