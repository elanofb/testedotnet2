using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteElano.Models;
using TesteElano.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace TesteElano.Repository
{
    public class ProjetoRepository: IProjetoRepository
    {
        TesteElanoContext db;

        public ProjetoRepository(TesteElanoContext _db) { 
            
            db = _db;

        }

        public async Task<List<ProjetoViewModel>> GetProjetos()
        {
            if (db != null)
            {                
                return (from p in db.Projeto
                             select new ProjetoViewModel
                              {
                                  ProjetoId = p.ProjetoId,                                  
                                  Nome = p.Nome,
                                  DtFim = p.DtFim,
                                  DtInicio = p.DtInicio,
                             }).ToList();
            }

            return null;
        }

        public async Task<ProjetoViewModel> GetProjeto(int? projetoId)
        {
            if (db != null)
            {
                return await (from p in db.Projeto
                              where p.ProjetoId == projetoId
                              select new ProjetoViewModel
                              {
                                  ProjetoId = p.ProjetoId,
                                  Nome = p.Nome,
                                  DtFim = p.DtFim,
                                  DtInicio = p.DtInicio
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddProjeto(Projeto projeto)
        {
            if (db != null)
            {
                await db.Projeto.AddAsync(projeto);
                await db.SaveChangesAsync();

                return projeto.ProjetoId;
            }

            return 0;
        }

        public async Task<int> DeleteProjeto(int? projetoId)
        {
            int result = 0;

            if (db != null)
            {             
                var projeto = await db.Projeto.FirstOrDefaultAsync(x => x.ProjetoId == projetoId);

                if (projeto != null)
                {                    
                    db.Projeto.Remove(projeto);                    
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateProjeto(Projeto projeto)
        {
            if (db != null)
            {
                db.Projeto.Update(projeto);                
                await db.SaveChangesAsync();
            }
        }
    }
}
