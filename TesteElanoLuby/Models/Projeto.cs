using System;
using System.Collections.Generic;

namespace TesteElano.Models
{
    public partial class Projeto
    {
        public Projeto()
        {
            Desenvolvedor = new HashSet<Desenvolvedor>();
            LancamentoHoras = new HashSet<LancamentoHoras>();
        }

        public int ProjetoId { get; set; }
        public string Nome { get; set; }
        public DateTime? DtInicio { get; set; }
        public DateTime? DtFim { get; set; }

        public virtual ICollection<Desenvolvedor> Desenvolvedor { get; set; }
        public virtual ICollection<LancamentoHoras> LancamentoHoras { get; set; }
    }
}
