using System;
namespace SyncAPI.Models
{
	public class Produto
	{
		public Guid Id { get; set; }
		public string NomeProduto { get; set; }
		public double ValorProduto { get; set; }
		public string InformacaoTecnica { get; set; }
		public byte[]? Foto { get; set; }
	}
}
