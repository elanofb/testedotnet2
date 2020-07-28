using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteElano.ViewModel
{
    public class DesenvolvedorViewModel
    {
        public int DesenvolvedorId { get; set; }
        public string Nome { get; set; }
        public DateTime? DtNascimento { get; set; }
        public string Cpf { get; set; }

        public int? ProjetoId { get; set; }
    }
}
