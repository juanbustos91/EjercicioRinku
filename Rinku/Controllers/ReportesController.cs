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
        public IEnumerable<Movimientos> Get(int mesIni, int mesFin, int idEmp)
        {
            Reporte repo = new Reporte(context);
            var movsFiltrados = repo.calculaRerporte(mesIni, mesFin, idEmp);

            return movsFiltrados.ToList();
        }

        // PUT api/<ReportesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}
