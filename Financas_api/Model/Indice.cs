using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas_api.Model
{
    public class Indice
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string Nome { get; set; }
        public DateTime DtAlteracao { get; set; }
    }
}
