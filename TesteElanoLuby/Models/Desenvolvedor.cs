using System;
using System.Collections.Generic;

namespace TesteElano.Models
{
    public partial class Desenvolvedor
    {
        public Desenvolvedor()
        {
            LancamentoHoras = new HashSet<LancamentoHoras>();
        }

        public int DesenvolvedorId { get; set; }
        public string Nome { get; set; }
        public DateTime? DtNascimento { get; set; }
        public string Cpf { get; set; }
        public int? ProjetoId { get; set; }

        public virtual Projeto Projeto { get; set; }
        public virtual ICollection<LancamentoHoras> LancamentoHoras { get; set; }

        //public string AccessKey { get; set; }
    }
}
