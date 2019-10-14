using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas_api.Model
{
    public class FundoDeInvestimento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public long CNPJ { get; set; }
        public decimal TxPerformance { get; set; }
        public decimal TxAdministracao { get; set; }
        public decimal TxImpRenda { get; set; }
        public decimal TxResgate { get; set; }
        public decimal QtdCotas { get; set; }
        public decimal ValorCota { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal PercIndice { get; set; }
        public Indice Benchmark { get; set; }
        public int ANBIMA { get; set; }
        public string ISIN { get; set; }
    }
}
