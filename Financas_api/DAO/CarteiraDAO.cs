using Financas_api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas_api.DAO
{
    public class CarteiraDAO : BaseDAO, ICadastravel<Carteira>
    {
        public int Adiciona(Carteira pObj)
        {
            try
            {
                //contexto.Database.EnsureCreated();
                contexto.Carteiras.Add(pObj);
                contexto.SaveChanges();
                return pObj.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Altera(Carteira pObj)
        {
            try
            {
                contexto.Carteiras.Update(pObj);
                contexto.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<Carteira> Carrega(int pId = 0, int pPrimeiraLinha = 0, int pNroLinhas = 0)
        {
            try
            {
                if (pId != 0)
                {
                    List<Carteira> lst = new List<Carteira>
                    {
                        contexto.Carteiras.Find(pId)
                    };
                    return lst;
                }
                if (pNroLinhas != 0)
                {
                    return contexto.Carteiras.OrderBy(p => p.Id).Skip(pPrimeiraLinha).Take(pNroLinhas).ToList();
                }
                else
                    return contexto.Carteiras.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<Carteira> CarregaFull(int pId = 0, int pPrimeiraLinha = 0, int pNroLinhas = 10)
        {
            try
            {
                if (pId != 0)
                {
                    return new List<Carteira>
                    {
                        contexto.Carteiras
                        .Include("Carteiras.Proprietario")
                        .Include("Carteiras.Fundos")
                        .FirstOrDefault(x => x.Id == pId)
                    };
                }
                if (pNroLinhas != 0)
                {
                    return contexto.Carteiras
                        .Include("Carteiras.Proprietario")
                        .Include("Carteiras.Fundos")
                        .OrderBy(p => p.Id)
                        .Skip(pPrimeiraLinha)
                        .Take(pNroLinhas).ToList();
                }
                else
                {
                    return contexto.Carteiras
                        .Include("Carteiras.Proprietario")
                        .Include("Carteiras.Fundos")
                        .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Remove(Carteira pObj)
        {
            try
            {
                contexto.Carteiras.Remove(pObj);
                contexto.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
