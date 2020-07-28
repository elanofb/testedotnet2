using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteElano.Models;

namespace TesteElano.ViewModel
{
    public class LancamentoViewModel
    {
        public int LancamentoHorasDesenvolvedorId { get; set; }
        
        public Desenvolvedor Desenvolvedor { get; set; }

        public Projeto Projeto { get; set; }

        public int? DesenvolvedorId { get; set; }
        
        public int? ProjetoId { get; set; }

        public DateTime? DtInicio { get; set; }
        
        public DateTime? DtFim { get; set; }

    }
}
