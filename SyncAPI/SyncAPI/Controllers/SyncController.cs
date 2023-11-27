using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyncAPI.Data;
using SyncAPI.Models;

namespace SyncAPI.Controllers
{
    [Route("api/[controller]")]
    public class SyncController : Controller
    {
        private readonly SyncAPIDbContext _dbContext;

        public SyncController(SyncAPIDbContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SyncData data)
        {
            if (data == null)
            {
                return BadRequest("Dados inválidos.");
            }

            foreach (var visita in data.Visitas)
            {
                if (!_dbContext.Visitas.Any(v => v.Id == visita.Id))
                {
                    _dbContext.Visitas.Add(visita);
                }
            }

            foreach (var produto in data.Produtos)
            {
                if (!_dbContext.Produtos.Any(p => p.Id == produto.Id))
                {
                    _dbContext.Produtos.Add(produto);
                }
            }

            await _dbContext.SaveChangesAsync();

            return Ok("Dados sincronizados com sucesso.");
        }
    }
}

