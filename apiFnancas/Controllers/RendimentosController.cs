using Financas_api.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace apiFnancas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RendimentosController : ControllerBase
    {        
        private readonly ILogger<RendimentosController> _logger;

        public RendimentosController(ILogger<RendimentosController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("evolucao")]
        public string GetEvolucao()
        {
            return JsonConvert.SerializeObject(new RendimentosDAO().GetEvolucaoPatrimonio());
        }
    }
}
