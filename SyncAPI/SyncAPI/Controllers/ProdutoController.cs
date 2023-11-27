using Microsoft.AspNetCore.Mvc;
using SyncAPI.Interfaces;
using SyncAPI.Models;
using SyncAPI.Services;
using System;
using System.Threading.Tasks;

namespace SyncAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProdutos()
        {
            var produtos = await _produtoService.GetAllProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduto(Guid id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduto([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _produtoService.AddProdutoAsync(produto);
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(Guid id, [FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != produto.Id)
                return BadRequest();

            await _produtoService.UpdateProdutoAsync(produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);
            if (produto == null)
                return NotFound();

            await _produtoService.DeleteProdutoAsync(id);
            return NoContent();
        }
    }
}
