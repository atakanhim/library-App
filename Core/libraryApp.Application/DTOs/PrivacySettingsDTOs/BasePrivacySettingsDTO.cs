using libraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.DTOs.PrivacySettingsDTOs
{
    public class BasePrivacySettingsDTO:BaseDTO
    {

        public bool IsPublic {  get; set; }
     
        public bool IsFriendsOnly { get; set; }
 
        public bool IsPrivate { get; set; }
     
       // public string NoteId { get; set; }
        //public Note Note { get; set; }
    }
}
