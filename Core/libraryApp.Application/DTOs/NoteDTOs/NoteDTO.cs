using libraryApp.Application.DTOs.BookDTOs;
using libraryApp.Application.DTOs.NoteDTOs;
using libraryApp.Application.DTOs.PrivacySettingsDTOs;
using libraryApp.Application.DTOs.User;
using libraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.DTOs.NoteDTOs
{
    public class NoteDTOIncludeNothing : BaseNoteDTO
    {

    }
    public class CreateNoteDTO : BaseNoteDTO
    {
        public string BookId { get; set; }
        public string UserId { get; set; }
    }

    public class UpdateNoteDTO : BaseNoteDTO
    {
    } 
    
    public class ListNoteDTO : BaseNoteDTO
    {
        public BookDTOIncludeNothing Book { get; set;}
        public ListUserDTO User { get; set;}
        public DefaultPrivacySettingsDTOInclude PrivacySettings { get; set; }
    }

}
