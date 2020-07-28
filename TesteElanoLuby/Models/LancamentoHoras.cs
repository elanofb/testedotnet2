using System;
using System.Collections.Generic;

namespace TesteElano.Models
{
    public partial class LancamentoHoras
    {
        public int LancamentoHorasDesenvolvedorId { get; set; }
        public int? DesenvolvedorId { get; set; }
        public int? ProjetoId { get; set; }
        public DateTime? DtInicio { get; set; }
        public DateTime? DtFim { get; set; }
        public virtual Desenvolvedor Desenvolvedor { get; set; }
        public virtual Projeto Projeto { get; set; }
    }
}
