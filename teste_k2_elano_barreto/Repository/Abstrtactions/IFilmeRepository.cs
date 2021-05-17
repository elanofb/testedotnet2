using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteElano.ViewModel;
using teste_k2_elano_barreto.Models;

namespace TesteElano.Repository
{
    public interface IFilmeRepository
    {
        List<FilmeViewModel> Get();

        Task<FilmeViewModel> GetFilme(int? filmeId);

        Task<int> AddFilme(Filme filme);

        Task<int> DeleteFilme(int? filmeId);

        Task UpdateFilme(Filme filme);
    }
}
