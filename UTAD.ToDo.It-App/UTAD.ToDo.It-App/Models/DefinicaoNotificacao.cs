﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTAD.ToDo.It_App.Models.Shared;

namespace UTAD.ToDo.It_App.Models
{
    public class DefinicaoNotificacao : BaseModel
    {
        public string Som { get; set; }

        public string Volume { get; set; }

        public string Repetição { get; set; }

    }
}