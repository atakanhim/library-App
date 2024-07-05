using libraryApp.Application.DTOs.NoteDTOs;
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

}
