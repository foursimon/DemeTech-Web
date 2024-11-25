namespace DemeTech.Models
{
	public class ItensCarrinho
	{
		private int? itensId;
		private string? codProduto { get; set; }

		private int? carrinhoId { get; set; }

		public virtual CarrinhoDeProduto? Carrinho { get; set; }

		public virtual Produto? CodigoProdutoNavigation { get; set; }

		public string? CodProduto { get { return codProduto; } set { codProduto = value; } }
		public int? CarrinhoId { get { return carrinhoId; } set { carrinhoId = value; } }
		public int? ItensId { get { return itensId; } set { itensId = value; } }
	}
}
