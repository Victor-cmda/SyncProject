using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SyncAPI.Models;
using Microsoft.EntityFrameworkCore;
using SyncAPI.Interfaces;
using SyncAPI.Data;

namespace SyncAPI.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly SyncAPIDbContext _context;

        public ProdutoService(SyncAPIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllProdutosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetProdutoByIdAsync(Guid id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProdutoAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProdutoAsync(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProdutoAsync(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
