using DemeTech.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace DemeTech.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly DemetechContext _context;

        public ProdutoController(DemetechContext context)
        {
            _context = context;
        }
        public IActionResult CatalogoDeProdutos(string mensagem)
        {
            List<Produto> alimentos = new List<Produto>();
            var banco = _context.AlimentosParaVendas.ToList();
            foreach (var item in banco)
            {

                Produto produto = new Produto();
                produto.CodigoAlimento = item.CodigoAlimento;
                produto.NomeDoProduto = item.NomeDoProduto;
                produto.Categoria = item.Categoria;
                produto.QuantidadeEstoque = item.QuantidadeEstoque;
                produto.Descricao = item.Descricao;
                produto.Peso = item.Peso;
                produto.Preco = item.Preco;
                produto.Imagem = item.Imagem;

                alimentos.Add(produto);
            }
            ViewBag.Message = mensagem;
            return View(alimentos);
        }

        [Authorize]
        public IActionResult Carrinho(string mensagem)
        {
            ClaimsPrincipal usuarioAtual = new ClaimsPrincipal();
            usuarioAtual = this.User;
            string id = usuarioAtual.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<ItensCarrinho> itensCarrinhos = new List<ItensCarrinho>();
            var procura = _context.Carrinho.FirstOrDefault(x => x.Cnpj == id);
            var carrinho = _context.ItensCarrinhos.Where(x => x.Carrinho == procura).ToList();
            var alimentoEmBd = _context.AlimentosParaVendas.ToList();
            foreach(var item in carrinho)
            {
                ItensCarrinho car = new ItensCarrinho();
                car.CodigoProdutoNavigation = item.CodigoProdutoNavigation;
                car.Carrinho = item.Carrinho;
                car.ItensId = item.ItensId;
                itensCarrinhos.Add(car);
            }
            ViewBag.Message = mensagem;
            ViewBag.QtdProduto = procura.QuantidadeDeProdutos;
            ViewBag.PrecoTotal = procura.PrecoTotal;
            return View(itensCarrinhos);
        }
        [Authorize]
        public IActionResult AdicionarProduto(string codigo)
        {
            var alimentoEmBd = _context.AlimentosParaVendas.SingleOrDefault(x => x.CodigoAlimento == codigo);
            ClaimsPrincipal usuarioAtual = new ClaimsPrincipal();
            usuarioAtual = this.User;
            string id = usuarioAtual.FindFirst(ClaimTypes.NameIdentifier).Value;
            var procura = _context.Carrinho.SingleOrDefault(x => x.Cnpj == id);
            CarrinhoDeProduto carrinhoCliente = new CarrinhoDeProduto();
            carrinhoCliente = procura;

            ItensCarrinho item = new ItensCarrinho();
            if (alimentoEmBd != null)
            {
                item.CodigoProdutoNavigation = alimentoEmBd;
                item.Carrinho = procura;
                carrinhoCliente.PrecoTotal += alimentoEmBd.Preco;
                carrinhoCliente.QuantidadeDeProdutos++;

                _context.ItensCarrinhos.Add(item);
                _context.Carrinho.Update(carrinhoCliente);
                _context.SaveChanges();

                string mensagem = "Produto adicionado ao carrinho";
                /*É necessário o new para que o método de redirecionar entenda que o parâmetro adicionado
                não seja um caminho para outra página.*/
                return RedirectToAction("CatalogoDeProdutos", new { mensagem });
            }
            else
            {
                string mensagem = "Produto indisponível";
                return RedirectToAction("CatalogoDeProdutos", new {mensagem});
            }
        }

        public IActionResult RemoverProduto(int id, int carrinhoId, double alimentoPreco)
        {
            var item = _context.ItensCarrinhos.SingleOrDefault(item => item.ItensId == id);
            var carrinhoCliente = _context.Carrinho.SingleOrDefault(carrinho => carrinho.CarrinhoId == carrinhoId);
            carrinhoCliente.PrecoTotal -= alimentoPreco;
            carrinhoCliente.QuantidadeDeProdutos--;
            _context.ItensCarrinhos.Remove(item);
            _context.Carrinho.Update(carrinhoCliente);
            _context.SaveChanges();
            string mensagem = "Produto removido do carrinho";
            return RedirectToAction("Carrinho", new { mensagem });
        }

    }
}
