using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTAD.ToDo.It_App.Models.Shared;

namespace UTAD.ToDo.It_App.Models
{
    public class Perfil : BaseModel
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Fotografia { get; set; }

        public string PalavraPasseAtual { get; set; }

        public string PalavraPasseNova { get; set; }
    }
}
