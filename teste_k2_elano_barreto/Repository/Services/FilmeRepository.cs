using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using TesteElano.Models;
using TesteElano.ViewModel;
using teste_k2_elano_barreto.Models;
//using teste_k2_elano_barreto.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace TesteElano.Repository
{
    public class FilmeRepository: IFilmeRepository
    {
        //TesteElanoContext db;
        DB_A46567_cotacaoContext db;

        //public FilmeRepository(TesteElanoContext _db) { 
        public FilmeRepository(DB_A46567_cotacaoContext _db) { 
            
            db = _db;

        }

        public List<FilmeViewModel> Get()
        {
            if (db != null)
            {
                var result = (from p in db.Filme
                        select new FilmeViewModel
                        {
                            FilmeId = p.FilmeId,
                            Titulo = p.Titulo,
                            Genero = p.Genero,
                            Sinopse = p.Sinopse,
                            Imagem = p.Imagem,
                            DataAluguel = p.DataAluguel,
                            DateEntrega = p.DateEntrega,
                        }).ToList();

                return result;
            }

            return null;
        }

        public async Task<FilmeViewModel> GetFilme(int? filmeId)
        {
            if (db != null)
            {
                return await (from p in db.Filme
                              where p.FilmeId == filmeId
                              select new FilmeViewModel
                              {
                                  FilmeId = p.FilmeId,
                                  Titulo = p.Titulo,
                                  Genero = p.Genero,
                                  Sinopse = p.Sinopse,
                                  Imagem = p.Imagem,
                                  DataAluguel = p.DataAluguel,
                                  DateEntrega = p.DateEntrega,
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddFilme(Filme filme)
        {
            if (db != null)
            {
                await db.Filme.AddAsync(filme);
                await db.SaveChangesAsync();

                return filme.FilmeId;
            }

            return 0;
        }

        public async Task<int> DeleteFilme(int? filmeId)
        {
            int result = 0;

            if (db != null)
            {
                //Achar lançaamento por filme id
                var filme = await db.Filme.FirstOrDefaultAsync(x => x.FilmeId == filmeId);

                if (filme != null)
                {
                    //Deletar filme
                    db.Filme.Remove(filme);

                    //Comitar
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateFilme(Filme filme)
        {
            if (db != null)
            {
                //Deletar lançamento
                db.Filme.Update(filme);

                //Comitar a transação
                await db.SaveChangesAsync();
            }
        }
    }
}
