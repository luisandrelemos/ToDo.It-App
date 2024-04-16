using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTAD.ToDo.It_App.Models.Shared;

namespace UTAD.ToDo.It_App.Models
{
    public enum TipoAlerta
    {
        MuitoImportante,
        Importante,
        PoucoImportante
    }

    public class Alerta : BaseModel
    {
        public string Mensagem { get; set; }

        public DateTime DataHora { get; set; }

        public bool Desligado { get; set; }

        public TipoAlerta Tipo { get; set; }
    }
}
