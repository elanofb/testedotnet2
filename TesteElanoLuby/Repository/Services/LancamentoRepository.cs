using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteElano.Models;
using TesteElano.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace TesteElano.Repository
{
    public class LancamentoRepository: ILancamentoRepository
    {
        TesteElanoContext db;

        public LancamentoRepository(TesteElanoContext _db) { 
            
            db = _db;

        }

        public async Task<List<LancamentoViewModel>> GetLancamentos()
        {
            if (db != null)
            {
                return (from p in db.LancamentoHoras
                        select new LancamentoViewModel
                        {
                            LancamentoHorasDesenvolvedorId = p.LancamentoHorasDesenvolvedorId,
                            DesenvolvedorId = p.DesenvolvedorId,
                            ProjetoId = p.ProjetoId,
                            DtFim = p.DtFim,
                            DtInicio = p.DtInicio,
                        }).ToList();
            }

            return null;
        }

        public async Task<LancamentoViewModel> GetLancamento(int? lancamentoId)
        {
            if (db != null)
            {
                return await (from p in db.LancamentoHoras
                              where p.LancamentoHorasDesenvolvedorId == lancamentoId
                              select new LancamentoViewModel
                              {
                                  LancamentoHorasDesenvolvedorId = p.LancamentoHorasDesenvolvedorId,
                                  DesenvolvedorId = p.DesenvolvedorId,
                                  ProjetoId = p.ProjetoId,
                                  DtFim = p.DtFim,
                                  DtInicio = p.DtInicio
                              }).FirstOrDefaultAsync();
            }

            return null;
        }
        
        public async Task<List<string>> GetRankingLancamentos()
        {
            var result = new List<string>();

            if (db != null)
            {
                int diaDaSemana = (int)DateTime.Now.DayOfWeek;

                //Lançamentos da Semana atual
                var lancamentosSemanaAtual = (from p in db.LancamentoHoras
                                             //from c in db.Category
                                         where p.DtInicio >= DateTime.Now.AddDays(-diaDaSemana)
                                         select new LancamentoViewModel
                                         {
                                             LancamentoHorasDesenvolvedorId = p.LancamentoHorasDesenvolvedorId,
                                             DesenvolvedorId = p.DesenvolvedorId,
                                             Desenvolvedor = p.Desenvolvedor,
                                             ProjetoId = p.ProjetoId,
                                             DtFim = p.DtFim,
                                             DtInicio = p.DtInicio,
                                             //}).ToListAsync();
                                         }).ToList();

                //Id e Quantidade de minutos
                var listaLancamentosDev = new Dictionary<int,double>();

                foreach (var lancamentosDev in lancamentosSemanaAtual)
                {                    
                    var qteMinutos = lancamentosDev.DtFim.Value - lancamentosDev.DtInicio.Value;
                    
                    var lanc = listaLancamentosDev.ContainsKey(lancamentosDev.DesenvolvedorId.Value);
                    
                    if (lanc)
                    {
                        listaLancamentosDev[lancamentosDev.DesenvolvedorId.Value] += qteMinutos.TotalMinutes;
                    }
                    else
                    {
                        listaLancamentosDev.Add(lancamentosDev.DesenvolvedorId.Value, qteMinutos.TotalMinutes);
                    }
                }

                foreach (var lanc in listaLancamentosDev.OrderByDescending(o => o.Value))
                {
                    var devInfo = lancamentosSemanaAtual.Where(l => l.DesenvolvedorId == lanc.Key).FirstOrDefault().Desenvolvedor;

                    TimeSpan ts = TimeSpan.FromMinutes(lanc.Value);
                    var str = devInfo.Nome + " - " + ts.ToString(@"hh\:mm\:ss");
                    result.Add(str);
                }

                return result.Take(5).ToList();
            }

            return null;
        }        

        public async Task<int> AddLancamento(LancamentoHoras lancamento)
        {
            if (db != null)
            {
                await db.LancamentoHoras.AddAsync(lancamento);
                await db.SaveChangesAsync();

                return lancamento.LancamentoHorasDesenvolvedorId;
            }

            return 0;
        }

        public async Task<int> DeleteLancamento(int? lancamentoId)
        {
            int result = 0;

            if (db != null)
            {
                //Achar lançaamento por lancamento id
                var lancamento = await db.LancamentoHoras.FirstOrDefaultAsync(x => x.LancamentoHorasDesenvolvedorId == lancamentoId);

                if (lancamento != null)
                {
                    //Deletar lancamento
                    db.LancamentoHoras.Remove(lancamento);

                    //Comitar
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateLancamento(LancamentoHoras lancamento)
        {
            if (db != null)
            {
                //Deletar lançamento
                db.LancamentoHoras.Update(lancamento);

                //Comitar a transação
                await db.SaveChangesAsync();
            }
        }
    }
}
