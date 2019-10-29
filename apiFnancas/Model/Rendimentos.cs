using System;

namespace Financas_api.Model
{
    public class Rendimentos
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public FundoDeInvestimento Fundo { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoFinal { get; set; }
        public decimal LuroApurado { get; set; }
        public decimal PercLucro { get; set; }
        public Carteira carteira { get; set; }
    }
}
