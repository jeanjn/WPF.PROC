using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProc.Model
{
    public class Andamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual IEnumerable<Processo> Processos { get; set; }
    }
}
