using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTAD.ToDo.It_App.Models.Shared;

namespace UTAD.ToDo.It_App.Models
{
    public class SignUp : BaseModel
    {
        public string id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
