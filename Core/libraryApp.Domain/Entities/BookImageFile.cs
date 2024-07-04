﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Domain.Entities
{
    public class BookImageFile : File
    {
        public string BookId { get; set; }
        public Book Book { get; set; }
    }
}
