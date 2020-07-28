using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteElano.Models;
using TesteElano.ViewModel;

namespace TesteElano.Repository
{
    public interface IDesenvolvedorRepository
    {
        Task<List<DesenvolvedorViewModel>> GetDesenvolvedores();
                
        DesenvolvedorViewModel GetDesenvolvedor(int? desenvolvedorId);

        int AddDesenvolvedor(Desenvolvedor desenvolvedor);

        Task<int> DeleteDesenvolvedor(int? desenvolvedorId);
                
        int UpdateDesenvolvedor(Desenvolvedor desenvolvedor);
    }
}
