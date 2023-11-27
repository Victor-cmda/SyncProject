using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SyncMobile.Context;
using SyncMobile.Models;

public class ProdutoService
{
    private readonly SyncContext _dbContext;

    public ProdutoService()
    {
        _dbContext = new SyncContext();
    }

    public async Task<List<Produto>> GetProdutosAsync()
    {
        return await _dbContext.Produtos.ToListAsync();
    }
}
