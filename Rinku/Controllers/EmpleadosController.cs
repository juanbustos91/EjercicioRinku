using Microsoft.AspNetCore.Mvc;
using Rinku.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rinku.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly RinkuContext context;

        public EmpleadosController(RinkuContext _context)
        {
            this.context = _context;
        }

        // GET: api/<EmpleadosController>
        [HttpGet]
        public IEnumerable<Empleados> Get()
        {
            return context.Empleados.ToList();
        }

        //[HttpGet]
        //[Route("max")]
        //public int max()
        //{
        //    //var max = (from e in context.Empleados select e).Max();
        //    //var max = (context.Empleados).Max();


        //    var max = (from d in context.Empleados select d.Id).Max();
                             

        //    List<int> lst = (from d in context.Empleados
        //                           orderby d.Id
        //                           select d.Id).ToList();


        //    return max;
        //}

        // GET api/<EmpleadosController>/5
        [HttpGet("{id}")]
        public Empleados Get(int id)
        {
            var empleado = context.Empleados.FirstOrDefault(p=>p.Id == id);
            return empleado; 
        }

        // POST api/<EmpleadosController>
        [HttpPost]
        public ActionResult Post([FromBody] Empleados empleado)
        {
            try
            {
                context.Empleados.Add(empleado);
                context.SaveChanges();

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<EmpleadosController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Empleados empleado)
        {
            if (empleado.Id == id)
            {
                context.Entry(empleado).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<EmpleadosController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var empleado = context.Empleados.FirstOrDefault(p => p.Id == id);

            if (empleado != null)
            {
                context.Empleados.Remove(empleado);
                context.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
