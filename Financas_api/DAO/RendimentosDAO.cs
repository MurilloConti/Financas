using Financas_api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Financas_api.DAO
{
    public class RendimentosDAO : BaseDAO, ICadastravel<Rendimentos>
    {
        public int Adiciona(Rendimentos pObj)
        {
            try
            {
                //contexto.Database.EnsureCreated();
                contexto.Rendimentos.Add(pObj);
                contexto.SaveChanges();
                return pObj.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Altera(Rendimentos pObj)
        {
            try
            {
                contexto.Rendimentos.Update(pObj);
                contexto.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IList<Rendimentos> Carrega(int pId = 0, int pPrimeiraLinha = 0, int pNroLinhas = 0)
        {
            try
            {
                if (pId != 0)
                {
                    List<Rendimentos> lst = new List<Rendimentos>
                    {
                        contexto.Rendimentos.Find(pId)
                    };
                    return lst;
                }
                if (pNroLinhas != 0)
                {
                    return contexto.Rendimentos.OrderBy(p => p.Id).Skip(pPrimeiraLinha).Take(pNroLinhas).ToList();
                }
                else
                    return contexto.Rendimentos.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IList<Rendimentos> CarregaFull(int pId = 0, int pPrimeiraLinha = 0, int pNroLinhas = 10)
        {
            try
            {
                if (pId != 0)
                {
                    return new List<Rendimentos>
                    {
                        contexto.Rendimentos
                        .Include("Rendimentos.Fundo")
                        .FirstOrDefault(x => x.Id == pId)
                    };
                }
                if (pNroLinhas != 0)
                {
                    return contexto.Rendimentos
                        .Include("Fundos.Fundo")
                        .OrderBy(p => p.Id)
                        .Skip(pPrimeiraLinha)
                        .Take(pNroLinhas).ToList();
                }
                else
                {
                    return contexto.Rendimentos
                        .Include("Fundos.Fundo")
                        .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Remove(Rendimentos pObj)
        {
            try
            {
                contexto.Rendimentos.Remove(pObj);
                contexto.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Dictionary<string,decimal> GetEvolucaoPatrimonio()
        {
            Dictionary<string, decimal> outPut = new Dictionary<string, decimal>();
            string sqlString = "select CONCAT(MONTH(DataFim),'/',YEAR(DataFim)) as mmYYYY,sum(SaldoFinal)  as total FROM rendimentos GROUP BY(DataFim)";
            using (var context = new FinancaContext())
            {
                var Evolucao = context.Rendimentos.FromSql(sqlString).ToList();
            }
            return outPut;
        }
    }
}
