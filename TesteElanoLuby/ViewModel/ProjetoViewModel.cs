using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteElano.ViewModel
{
    public class ProjetoViewModel
    {
        public int ProjetoId { get; set; }
        public string Nome { get; set; }
        public DateTime? DtInicio { get; set; }
        public DateTime? DtFim { get; set; }
    }
}


