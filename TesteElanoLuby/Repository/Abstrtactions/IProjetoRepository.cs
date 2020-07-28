using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteElano.Models;
using TesteElano.ViewModel;

namespace TesteElano.Repository
{
    public interface IProjetoRepository
    {
        Task<List<ProjetoViewModel>> GetProjetos();

        Task<ProjetoViewModel> GetProjeto(int? projetoId);

        Task<int> AddProjeto(Projeto projeto);

        Task<int> DeleteProjeto(int? projetoId);

        Task UpdateProjeto(Projeto projeto);
    }
}
