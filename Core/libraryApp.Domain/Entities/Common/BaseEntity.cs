﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryApp.Domain.Entities.Common
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; } 
        virtual public DateTime UpdatedDate { get; set; } // belki kullanmak istemeyiz diye ezilebilir yapıyoruz
    }
}
