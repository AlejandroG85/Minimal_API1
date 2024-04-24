using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Minimal_API.Data;
using Minimal_API.Models;

namespace Minimal_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TareasController : Controller
    {
        private readonly Minimal_APIData _context;
        private readonly IMemoryCache _cache;

        public TareasController(Minimal_APIData context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet]
        public IEnumerable<Tareas> Get()
        {
            return _context.Tareas.OrderBy(t => t.Orden).AsEnumerable();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Tareas tarea)
        {
            _context.Tareas.Add(tarea);
            _context.SaveChanges();

            _cache.Remove("tarea");

            return Ok(tarea);
        }

        [HttpPost("{id}/Hecho")]
        public IActionResult Finalizar_Tarea(int id)
        {
            var tarea = _context.Tareas.Where(w => w.Id == id).FirstOrDefault();
            if (tarea == null) return NotFound();

            tarea.Finalizada = !tarea.Finalizada; //true;
            _context.SaveChanges();

            _cache.Remove("tarea");

            return Ok(tarea);
        }

        [HttpPost("OrdenarTareas")]
        public IActionResult Actualizar_Orden(string order)
        {
            var ids = order.Split(',').Select(int.Parse).ToList();
            var tareas = _context.Tareas.ToList();

            for (int i = 0; i < ids.Count; i++)
            {
                var tarea = tareas.FirstOrDefault(t => t.Id == ids[i]);
                if (tarea != null)
                {
                    tarea.Orden = i + 1;
                }
            }

            _context.SaveChanges();
            _cache.Remove("tarea");

            return Ok();
        }

        [HttpPost("Reordenar")]
        public IActionResult Ordenar_Tarea(List<Tareas> tareas)
        {
            foreach (Tareas tarea in tareas)
            {
                var mi_tarea = _context.Tareas.Where(w => w.Id == tarea.Id).First();
                mi_tarea.Orden = tarea.Orden;
            }

            _context.SaveChanges();

            _cache.Remove("tarea");

            return Ok(tareas);
        }
    }
}
