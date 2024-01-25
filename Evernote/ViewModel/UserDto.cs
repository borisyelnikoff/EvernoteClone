using Evernote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evernote.ViewModel
{
    public class UserDto : User
    {
        public string ConfirmedPassword { get; set; }
    }
}
