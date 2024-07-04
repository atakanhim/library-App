using libraryApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Domain.Entities
{
    public class Shelf:BaseEntity
    {
        public string Name { get; set; } // rafın ismi
        public ICollection<Book> Books { get; set; } // raftaki kitaplar
    }
}
