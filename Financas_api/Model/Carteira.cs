using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas_api.Model
{
    public class Carteira
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Pessoa Proprietario { get; set; }
        public List<FundoDeInvestimento> Fundos { get; set; }
    }
}
