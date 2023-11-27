using System;
using SyncAPI.Models;

namespace SyncAPI.Interfaces
{
	public interface IProdutoService
	{
        Task<IEnumerable<Produto>> GetAllProdutosAsync();
        Task<Produto> GetProdutoByIdAsync(Guid id);
        Task AddProdutoAsync(Produto produto);
        Task UpdateProdutoAsync(Produto produto);
        Task DeleteProdutoAsync(Guid id);
    }
}

