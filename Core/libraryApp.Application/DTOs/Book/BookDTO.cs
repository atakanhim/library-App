using libraryApp.Application.DTOs.Shelf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Application.DTOs.Book
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
