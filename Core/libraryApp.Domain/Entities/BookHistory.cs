using libraryApp.Domain.Entities.Common;
using libraryApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Domain.Entities
{
    public class BookHistory:BaseEntity
    {
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public DateTime ActionDate { get; set; }
        public BookAction Action { get; set; }
    }
    public enum BookAction
    {
        AddedToLibrary,
        CheckedOut,
        Returned
    }
}
