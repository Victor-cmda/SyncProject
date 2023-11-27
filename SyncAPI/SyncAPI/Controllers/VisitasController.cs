using Microsoft.AspNetCore.Mvc;
using SyncAPI.Models;
using SyncAPI.Services;
using System;
using System.Threading.Tasks;

namespace SyncAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisitasController : ControllerBase
    {
        private readonly IVisitasService _visitasService;

        public VisitasController(IVisitasService visitasService)
        {
            _visitasService = visitasService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVisitas()
        {
            var visitas = await _visitasService.GetAllVisitasAsync();
            return Ok(visitas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisita(Guid id)
        {
            var visita = await _visitasService.GetVisitaByIdAsync(id);
            if (visita == null)
                return NotFound();

            return Ok(visita);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisita([FromBody] Visitas visita)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _visitasService.AddVisitaAsync(visita);
            return CreatedAtAction(nameof(GetVisita), new { id = visita.Id }, visita);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVisita(Guid id, [FromBody] Visitas visita)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != visita.Id)
                return BadRequest();

            await _visitasService.UpdateVisitaAsync(visita);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisita(Guid id)
        {
            var visita = await _visitasService.GetVisitaByIdAsync(id);
            if (visita == null)
                return NotFound();

            await _visitasService.DeleteVisitaAsync(id);
            return NoContent();
        }
    }
}
