using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Drawing;

namespace DemeTech.Models
{
    public class Produto
    {
        private string? codigoAlimento;
        private string? nomeDoProduto;
        private string? descricao;
        private double? preco;
        private string? categoria;
        private int? quantidadeEstoque;
        private double? peso;
        private byte[]? imagem;

        public byte[]? Imagem { get { return imagem; } set { imagem = value; } }

        public Produto() {

        }

        public string? CodigoAlimento { get { return codigoAlimento; } set { codigoAlimento = value; } }
        public string? NomeDoProduto { get { return nomeDoProduto; } set { nomeDoProduto = value; } }
        public string? Descricao { get { return descricao; } set { descricao = value; } }
        public double? Preco { get { return preco; } set { preco = value; } } 
        public string? Categoria { get { return categoria; } set {   categoria = value; } }
        
        public int? QuantidadeEstoque { get { return quantidadeEstoque; } set { quantidadeEstoque = value; } }
        public double? Peso { get { return peso; } set { peso = value; } }
    }
}
