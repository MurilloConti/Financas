using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas_api.Model
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public long CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
