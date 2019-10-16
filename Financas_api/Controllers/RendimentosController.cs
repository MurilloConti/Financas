using Financas_api.DAO;

namespace Financas_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RendimentosController : ControllerBase
    {
        [Route("rendimentos/evolucao")]
        [HttpGet]
        public ActionResult<string> Get()
        {
            RendimentosDAO rendDAo = new RendimentosDAO();
            var resp = rendDAo.GetEvolucaoPatrimonio();
            return JsonConvert.SerializeObject(resp);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
