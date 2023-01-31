using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rinku.Models;

namespace Rinku.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly RinkuContext _context;

        public MovimientosController(RinkuContext context)
        {
            _context = context;
        }

        // GET: api/Movimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimientos>>> GetMovimientos()
        {
            var tabMovs = _context.Movimientos.ToList();
            IEnumerable<Movimientos> movs;

            movs = from m in tabMovs orderby m.Id descending select m;

            return movs.ToList();
        }

        // GET: api/Movimientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimientos>> GetMovimientos(int id)
        {
            var movimientos = await _context.Movimientos.FindAsync(id);

            if (movimientos == null)
            {
                return NotFound();
            }

            return movimientos;
        }

        // PUT: api/Movimientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimientos(int id, Movimientos movimientos)
        {
            if (id != movimientos.Id)
            {
                return BadRequest();
            }

            var intNumEmp = int.Parse(movimientos.NumEmpleado);
            var emp = await _context.Empleados.FindAsync(intNumEmp);

            if (emp == null)
            {
                return NotFound("Empleado no existe");
            }

            MovsExtendido movsExt = new MovsExtendido(movimientos);
            movsExt.calcularSueldo(id, emp);

            await _context.Procedures.spActualizarMovimientosAsync(id, movsExt.NumEmpleado,
                movsExt.Mes, movsExt.CantidadEntregas, movsExt.SueldoBruto, movsExt.Isr, movsExt.IsrAdicional, movsExt.Vales, movsExt.SueldoNeto);
            //_context.Entry(movimientos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Movimientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movimientos>> PostMovimientos(Movimientos movimientos)
        {
            var intNumEmp = int.Parse(movimientos.NumEmpleado);
            var emp = await _context.Empleados.FindAsync(intNumEmp);

            if (emp == null)
            {
                return NotFound("Empleado no existe");
            }

            MovsExtendido movsExt = new MovsExtendido(movimientos);
            movsExt.calcularSueldo(0, emp);

            await _context.Procedures.spInsertaMovimientosAsync(movsExt.NumEmpleado,
                movsExt.Mes, movsExt.CantidadEntregas, movsExt.SueldoBruto, movsExt.Isr, movsExt.IsrAdicional, movsExt.Vales, movsExt.SueldoNeto);
            //_context.Movimientos.Add(movimientos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovimientos", new { id = movimientos.Id }, movimientos);
        }


        // DELETE: api/Movimientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimientos(int id)
        {
            var movimientos = await _context.Movimientos.FindAsync(id);
            if (movimientos == null)
            {
                return NotFound();
            }

            _context.Movimientos.Remove(movimientos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovimientosExists(int id)
        {
            return _context.Movimientos.Any(e => e.Id == id);
        }
    }
}
