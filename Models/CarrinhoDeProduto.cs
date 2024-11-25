using System.ComponentModel.DataAnnotations;

namespace DemeTech.Models
{
    public class CarrinhoDeProduto
    {
        private int? carrinhoId;
        private double? precoTotal;
        private int? quantidadeDeProdutos;
    
        private string? cnpj;
        
		public virtual Cliente? CnpjNavigation { get; set; }
		public CarrinhoDeProduto() { }

        [Key]
        public int? CarrinhoId { get { return carrinhoId; } set { carrinhoId = value; } }
        public string? Cnpj { get { return cnpj; } set { cnpj = value; } }
        public double? PrecoTotal { get { return precoTotal; } set { precoTotal = value; } }
        public int? QuantidadeDeProdutos { get { return quantidadeDeProdutos; } set { quantidadeDeProdutos = value; } }


    }
}
