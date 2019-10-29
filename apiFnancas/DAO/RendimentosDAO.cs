using apiFnancas.Model;
using Financas_api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public void InsereRendimentoProc(DateTime dtFinal, int idFindo, decimal valAtual)
        {
            using (var context = new FinancaContext())
            {
                using (var cmd = context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "sp_Carteira_Inserts";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                          
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("dtFinal", dtFinal.ToString("yyyyMMdd")));
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("idFundo", idFindo));
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("valoAtual", valAtual));
                    context.Database.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<EvolucaoRendiemnto> GetEvolucaoPatrimonio()
        {            
            string sqlString = "select CONCAT(MONTH(DataFim),'/',YEAR(DataFim)) as DiaMes,sum(SaldoFinal)  as Total FROM rendimentos GROUP BY(DataFim)";
            using (var context = new FinancaContext())
            {
                return context.EvolucaoRendiemntos.FromSql(sqlString).ToList();
            }            
        }
        public List<EvolucaoRendiemnto> GetEvolucaoPatrimonioPercentual()
        {
            string sqlString = "select CONCAT(MONTH(DataFim),'/',YEAR(DataFim)) as mmYYYY, IFNULL(TRUNCATE(((sum(SaldoFinal) - sum(SaldoAnterior))*100)/sum(SaldoAnterior),2),0) as percentual from rendimentos GROUP BY(DataFim);";
            using (var context = new FinancaContext())
            {
                return context.EvolucaoRendiemntos.FromSql(sqlString).ToList();
            }
        }
        public decimal GetRendimentoCarteira()
        {
            decimal total = 0;
            using (var context = new FinancaContext())
            {                             
                using (var cmd = context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "sp_GetRendimentoCarteira";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                                                                 
                    context.Database.OpenConnection();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while(rdr.Read())
                        {
                            total = rdr.GetDecimal(0);
                        }
                    }
                }
            }   
            return total;
        }
    }
}
