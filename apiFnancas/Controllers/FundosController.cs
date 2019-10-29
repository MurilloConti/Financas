using Financas_api.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace apiFnancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FundosController
    {

        private readonly ILogger<FundosController> _logger;

        public FundosController(ILogger<FundosController> logger)
        {
            _logger = logger;
        }

        [HttpGet]        
        public string GetFundos()
        {
            return JsonConvert.SerializeObject(new FundoDeInvestimentoDAO().Carrega());
        }
    }
}
