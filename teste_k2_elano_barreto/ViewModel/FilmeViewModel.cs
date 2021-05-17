using System;
using System.Collections.Generic;

namespace TesteElano.ViewModel
{
    public partial class FilmeViewModel
    {
        public int FilmeId { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public string Imagem { get; set; }
        public string Genero { get; set; }
        public bool? Alugado { get; set; }
        public DateTime? DataAluguel { get; set; }
        public DateTime? DateEntrega { get; set; }
    }
}
