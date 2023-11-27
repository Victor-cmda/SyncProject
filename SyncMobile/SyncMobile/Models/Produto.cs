using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SyncMobile.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string NomeProduto { get; set; }
        public double ValorProduto { get; set; }
        public string InformacaoTecnica { get; set; }
        public byte[] Foto { get; set; }

        [NotMapped]
        public ImageSource ImageSource { get; set; }
    }
}
