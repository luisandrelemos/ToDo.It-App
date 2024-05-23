using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicação_ToDo.IT.SaveData
{
    public class UserData
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Tema { get; set; }
    }

    public class CurrentUser
    {
        public static UserData User { get; set; }
    }
}
