using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas_api.DAO
{
    public class BaseDAO : IDisposable
    {
        public BaseDAO()
        {
            contexto = new FinancaContext();
        }
        /// <summary>
        /// Context Entity Framework
        /// </summary>
        protected FinancaContext contexto;

        public void Dispose()
        {
            contexto.Dispose();
        }       
    }
}
