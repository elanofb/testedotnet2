using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace teste_k2_elano_barreto.Models
{
    [Table("Filme")]
    public partial class Filme
    {
        [Column("FilmeId")]
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
