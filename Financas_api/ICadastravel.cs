using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas_api
{
    interface ICadastravel<T> where T : class
    {
        int Adiciona(T pObj);
        void Altera(T pObj);
        void Remove(T pObj);
        IList<T> Carrega(int pId = 0, int pPrimeiraLinha = 0, int pNroLinhas = 0);
    }
}
