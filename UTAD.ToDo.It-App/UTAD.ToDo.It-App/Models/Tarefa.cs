using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTAD.ToDo.It_App.Models.Shared;

namespace UTAD.ToDo.It_App.Models
{
    public class Tarefa : BaseModel
    {
        public int id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public int DataInicio { get; set; }

        public int DataFim { get; set; }

        public int HoraInicio { get; set; }

        public int HoraFim { get; set; }

        public string NivelImportancia { get; set; }

        public string Estado { get; set; }

        public string Periodicidade { get; set; }

        public string AlertaAntecipacao { get; set; }

        public string AlertaExecucao { get; set; }

    }
}
