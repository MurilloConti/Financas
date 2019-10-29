using Financas_api.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

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

        [HttpGet]
        [Route("total")]
        public string GetrendimentoCarteira()
        {
            return JsonConvert.SerializeObject(new RendimentosDAO().GetRendimentoCarteira());
        }

        [HttpGet]
        [Route("evolucao/percentual")]
        public string GetEvolucaoPercentual()
        {            
            return JsonConvert.SerializeObject(new RendimentosDAO().GetEvolucaoPatrimonio());
        }
        [HttpPut]
        [Route("insere/{idFundo}/{valorFinal}/{dtFim}")]
        public void InsereRendimento(int idFundo, string dtFim,decimal valorFinal)
        {
            var data = Convert.ToDateTime(dtFim);
            new RendimentosDAO().InsereRendimentoProc(data, idFundo, valorFinal);
        }
    }
}
