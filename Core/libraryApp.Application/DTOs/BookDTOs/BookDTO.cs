using libraryApp.Application.DTOs.ShelfDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.DTOs.BookDTOs
{
    public class BookDTOIncludeNothing : BaseBookDTO
    {
    }  
    
    public class BookDTOIncludeShelf : BaseBookDTO
    {
       public ShelfDTOIncludeNothing Shelf { get; set; }

    }
    public class CreateBookDTO : BaseBookDTO
    {

    }
  
}
