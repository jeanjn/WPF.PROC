﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROC.Model
{
    public class Processo
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Numero { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public DateTime? Lembrete { get; set; }
        public int AndamentoId { get; set; }
        public virtual Andamento Andamento { get; set; }
    }
}
