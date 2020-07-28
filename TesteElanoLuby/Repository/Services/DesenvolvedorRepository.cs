using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteElano.Models;
using TesteElano.ViewModel;

namespace TesteElano.Repository
{
    public class DesenvolvedorRepository: IDesenvolvedorRepository
    {
        TesteElanoContext db;

        public DesenvolvedorRepository(TesteElanoContext _db) {
            db = _db;
        }

        public async Task<List<DesenvolvedorViewModel>> GetDesenvolvedores()
        {
            if (db != null)
            {                
                return (from p in db.Desenvolvedor
                        select new DesenvolvedorViewModel
                        {
                            DesenvolvedorId = p.DesenvolvedorId,
                            Nome = p.Nome,
                            DtNascimento = p.DtNascimento,
                            Cpf = p.Cpf,
                            ProjetoId = p.ProjetoId
                        }).ToList();
            }

            return null;

        }

        public DesenvolvedorViewModel GetDesenvolvedor(int? desenvolvedorId)
        {
            if (db != null)
            {
                return (from p in db.Desenvolvedor
                              where p.DesenvolvedorId == desenvolvedorId
                              select new DesenvolvedorViewModel
                              {
                                  DesenvolvedorId = p.DesenvolvedorId,
                                  Nome = p.Nome,
                                  DtNascimento = p.DtNascimento,
                                  Cpf = p.Cpf,
                                  ProjetoId = p.ProjetoId
                              }).FirstOrDefault();
             
            }

            return null;
        }

        public int AddDesenvolvedor(Desenvolvedor desenvolvedor)
        {
            if (db != null)
            {
                db.Desenvolvedor.Add(desenvolvedor);
                db.SaveChanges();

                return desenvolvedor.DesenvolvedorId;
            }

            return 0;
        }

        public async Task<int> DeleteDesenvolvedor(int? desenvolvedorId)
        {
            int result = 0;

            if (db != null)
            {
                //Achar o desenvolvedor por desenvolvedor id
                var desenvolvedor = db.Desenvolvedor.FirstOrDefault(x => x.DesenvolvedorId == desenvolvedorId);

                if (desenvolvedor != null)
                {
                    //Deletar o desenvolvedor
                    db.Desenvolvedor.Remove(desenvolvedor);

                    //Commitar a transação
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public int UpdateDesenvolvedor(Desenvolvedor desenvolvedor)
        {
            if (db != null)
            {
                //Deletar o desenvolvedor
                db.Desenvolvedor.Update(desenvolvedor);

                //Commitar a transação
                db.SaveChanges();                
            }

            return desenvolvedor.DesenvolvedorId;
        }
    }
}
