﻿using Fatec.Clinica.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fatec.Clinica.Api.Model
{
    public class AtendimentoInput
    {
        public int IdClinica { get; set; }
        public int IdMedico { get; set; }
    }
}
