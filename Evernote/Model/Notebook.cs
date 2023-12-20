﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evernote.Model
{
    public class Notebook
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        public User User { get; set; }
    }
}
