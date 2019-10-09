using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas_api.Model
{
    public class Rendimento
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public FundoDeInvestimento Fundo { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoFinal { get; set; }
        public decimal LuroApurado { get; set; }
        public decimal PercLucro { get; set; }
    }
}
