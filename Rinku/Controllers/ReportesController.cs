using Microsoft.AspNetCore.Mvc;
using Rinku.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rinku.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {

        private readonly RinkuContext context;

        public ReportesController(RinkuContext _context)
        {
            this.context = _context;
        }

        // GET: api/<ReportesController>
        [HttpGet]
        [Route("/global")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ReportesController>/5
        [HttpGet]
        public Reporte Get(int mes, int id)
        {
            Reporte repo = new Reporte();
            repo.calculaRerporte(mes, id);

            return repo;
        }

        // POST api/<ReportesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReportesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReportesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
