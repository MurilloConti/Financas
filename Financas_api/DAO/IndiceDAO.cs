using Financas_api.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Financas_api.DAO
{
    public class IndiceDAO : BaseDAO, ICadastravel<Indice>
    {
        public int Adiciona(Indice pObj)
        {
            try
            {
                //contexto.Database.EnsureCreated();
                contexto.Indices.Add(pObj);
                contexto.SaveChanges();
                return pObj.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Altera(Indice pObj)
        {
            try
            {
                contexto.Indices.Update(pObj);
                contexto.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<Indice> Carrega(int pId = 0, int pPrimeiraLinha = 0, int pNroLinhas = 0)
        {
            try
            {
                if (pId != 0)
                {
                    List<Indice> lst = new List<Indice>
                    {
                        contexto.Indices.Find(pId)
                    };
                    return lst;
                }
                if (pNroLinhas != 0)
                {
                    return contexto.Indices.OrderBy(p => p.Id).Skip(pPrimeiraLinha).Take(pNroLinhas).ToList();
                }
                else
                    return contexto.Indices.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }        

        public void Remove(Indice pObj)
        {
            try
            {
                contexto.Indices.Remove(pObj);
                contexto.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
