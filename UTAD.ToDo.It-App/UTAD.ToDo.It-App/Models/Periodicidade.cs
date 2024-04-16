using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTAD.ToDo.It_App.Models.Shared;

namespace UTAD.ToDo.It_App.Models
{
    public class Periodicidade : BaseModel
    {
        public string Tipo { get; set; }

        public string DiaDaSemana { get; set; }
    }
}
