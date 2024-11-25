using Microsoft.AspNetCore.Mvc;
using DemeTech.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
namespace DemeTech.Controllers
{
    public class ContaController : Controller
    {
		private readonly DemetechContext _context;


		public ContaController(DemetechContext context)
		{
			_context = context;
		}

        public IActionResult Registrar()
        {
            ModelState.Clear();
			return View();
        }
		public IActionResult EfetuarLogin()
        {
			return View();
		}

        //Página só acessada se o cliente for autorizado
        [HttpPost]
        public IActionResult EfetuarLogin(ClienteLogar conta)
		{ 
			//O ModelState.IsValid verifica se os dados recebidos são válidos de acordo com as restrições
			//de validação definidas na classe do objeto.
            if (!ModelState.IsValid)
            {
				return View();
			}
            else
            {
				//Como o tipo numeric é decimal, foi necessário mudar cnpj para decimal para esta
				//verificação funcionar.
				//O método abaixo procura o primeiro cnpj e a primeira senha que for igual ao informado
				//pelo usuário.
				//Se achar a conta, o usuário será logado. Caso o contrário, uma mensagem de erro irá
				//aparecer para ele.

				var usuario = _context.Clientes.Where(x => x.Cnpj == conta.Cnpj && x.Senha == conta.Senha).FirstOrDefault();
                if(usuario != null)
                {
					ViewBag.Message = "Login realizado com sucesso";
					ModelState.Clear();
					//Adicionando o email do usuário como o nome a ser exibido.
					var receba = new List<Claim>
					{
						new Claim(ClaimTypes.NameIdentifier, usuario.Cnpj),
						new Claim(ClaimTypes.Name, usuario.Email)
					};
					//Irá logar o usuário e criar seus cookies
					var recebaIdentidade = new ClaimsIdentity(receba, CookieAuthenticationDefaults.AuthenticationScheme);
					HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(recebaIdentidade));
					return Redirect("../Produto/CatalogoDeProdutos");
				}
				else
				{
					ViewBag.Message = "Login ou senha errados.";
					ModelState.Clear();
					return View();
				}

			}
        }

        [HttpPost]
        public IActionResult Registrar(Cliente conta)
        {
            //O ModelState.IsValid verifica se os dados recebidos são válidos de acordo com as restrições
            //de validação definidas na classe do objeto.
            if (!ModelState.IsValid)
			{
				return View(conta);
            }
            else
            {
				//Procura na base de dados se o cnpj já está registrado.
				//Se não estiver registrado, o método Find() irá retornar null.
				var procura = _context.Clientes.Find(conta.Cnpj);

				//Criando a conta do cliente junto ao carrinho único dele.
				//Depois que a criação for um sucesso, será retornado uma mensagem informando o usuário
				//que o procedimento foi um sucesso.
				if (procura == null)
				{
					Cliente cliente = new Cliente();
					cliente.Cnpj = conta.Cnpj;
					cliente.Nome = conta.Nome;
					cliente.Email = conta.Email;
					cliente.Endereco = conta.Endereco;
					cliente.Senha = conta.Senha;
					cliente.Numero = conta.Numero;
					_context.Clientes.Add(cliente);
					CarrinhoDeProduto carrinho = new CarrinhoDeProduto();
					carrinho.Cnpj = cliente.Cnpj;
					carrinho.QuantidadeDeProdutos = 0;
					carrinho.PrecoTotal = 0;
					_context.Carrinho.Add(carrinho);
					_context.SaveChanges();
					ViewBag.Message = "Cadastro realizado com sucesso.";
					ModelState.Clear();
					return View();
				}
				ViewBag.Message = "Esse Cnpj já está cadastrado.";
				return View();
			}
			
        }

		[HttpPost]
		public IActionResult EfetuarLogout()
		{
			//Desconectando a conta do usuário
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return Redirect("../Home/Index");
		}


	}
}
