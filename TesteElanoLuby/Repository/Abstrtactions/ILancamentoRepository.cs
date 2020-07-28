using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteElano.Models;
using TesteElano.ViewModel;

namespace TesteElano.Repository
{
    public interface ILancamentoRepository
    {
        Task<List<LancamentoViewModel>> GetLancamentos();

        Task<LancamentoViewModel> GetLancamento(int? lancamentoId);

        Task<List<string>> GetRankingLancamentos();        

        Task<int> AddLancamento(LancamentoHoras lancamento);

        Task<int> DeleteLancamento(int? lancamentoId);

        Task UpdateLancamento(LancamentoHoras lancamento);
    }
}
