using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTAD.ToDo.It_App.Models.Shared;

namespace UTAD.ToDo.It_App.Models
{
    public class Calendario : BaseModel
    {
        public int Dia { get; set; }

        public int Semana { get; set; }

        public int Mes { get; set; }

        public int Ano { get; set; }
    }
}

