using Financas_api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Financas_api.DAO
{
    public class FundoDeInvestimentoDAO : BaseDAO, ICadastravel<FundoDeInvestimento>
    {
        public int Adiciona(FundoDeInvestimento pObj)
        {
            try
            {
                //contexto.Database.EnsureCreated();
                contexto.Fundos.Add(pObj);
                contexto.SaveChanges();
                return pObj.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Altera(FundoDeInvestimento pObj)
        {
            try
            {
                contexto.Fundos.Update(pObj);
                contexto.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<FundoDeInvestimento> Carrega(int pId = 0, int pPrimeiraLinha = 0, int pNroLinhas = 0)
        {
            try
            {
                if (pId != 0)
                {
                    List<FundoDeInvestimento> lst = new List<FundoDeInvestimento>
                    {
                        contexto.Fundos.Find(pId)
                    };
                    return lst;
                }
                if (pNroLinhas != 0)
                {
                    return contexto.Fundos.OrderBy(p => p.Id).Skip(pPrimeiraLinha).Take(pNroLinhas).ToList();
                }
                else
                    return contexto.Fundos.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<FundoDeInvestimento> CarregaFull(int pId = 0, int pPrimeiraLinha = 0, int pNroLinhas = 10)
        {
            try
            {
                if (pId != 0)
                {
                    return new List<FundoDeInvestimento>
                    {
                        contexto.Fundos
                        .Include("Fundos.Benchmark")                       
                        .FirstOrDefault(x => x.Id == pId)
                    };
                }
                if (pNroLinhas != 0)
                {
                    return contexto.Fundos
                        .Include("Fundos.Benchmark")                        
                        .OrderBy(p => p.Id)
                        .Skip(pPrimeiraLinha)
                        .Take(pNroLinhas).ToList();
                }
                else
                {
                    return contexto.Fundos
                        .Include("Fundos.Benchmark")                        
                        .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Remove(FundoDeInvestimento pObj)
        {
            try
            {
                contexto.Fundos.Remove(pObj);
                contexto.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
