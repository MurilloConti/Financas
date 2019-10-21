using Financas_api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas_api.DAO
{
    public class PessoaDAO : BaseDAO, ICadastravel<Pessoa>
    {

        public int Adiciona(Pessoa pObj)
        {
            try
            {
                //contexto.Database.EnsureCreated();
                contexto.Pessoas.Add(pObj);
                contexto.SaveChanges();
                return pObj.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Altera(Pessoa pObj)
        {
            try
            {
                contexto.Pessoas.Update(pObj);
                contexto.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<Pessoa> Carrega(int pId = 0, int pPrimeiraLinha = 0, int pNroLinhas = 0)
        {
            try
            {
                if (pId != 0)
                {
                    List<Pessoa> lst = new List<Pessoa>
                    {
                        contexto.Pessoas.Find(pId)
                    };
                    return lst;
                }
                if (pNroLinhas != 0)
                {
                    return contexto.Pessoas.OrderBy(p => p.Id).Skip(pPrimeiraLinha).Take(pNroLinhas).ToList();
                }
                else
                    return contexto.Pessoas.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
      
        public void Remove(Pessoa pObj)
        {
            try
            {                
                contexto.Pessoas.Remove(pObj);
                contexto.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
