using Azure;
using DemeTech.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Numerics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;

namespace DemeTech.Models;

public class Cliente
{
    //O interrogação transforma os atributos em nuláveis.
    //Isso serve para a restrição [Required] funcionar.
	private string? cnpj;
	private string? nome;
	private string? senha;
    private string? endereco;
	private string? email;
	private string? numero;

	public virtual ICollection<CarrinhoDeProduto> Carrinhos { get; set; } = new List<CarrinhoDeProduto>();
	public Cliente()
    {
        
    }

    //Validação de dados inseridos pelo usuário no registro de conta
	[Required(ErrorMessage = "Cnpj é obrigatório")] //Obriga o preenchimento deste dado.
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "Insira apenas números")] //Define que apenas números são permitidos.
    [StringLength(14, MinimumLength = 14, ErrorMessage = "Cnpj só pode ter 14 dígitos")] //Define o máximo e o mínimo de caracteres permitidos.
	public string? Cnpj
    {
        get { return cnpj; }
        set { cnpj = value; }
    }
	[Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, ErrorMessage = "Nome não pode exceder 100 caracteres")]
	public string? Nome
    {
        get { return nome; }
        set { nome = value; }
    }
	[Required(ErrorMessage = "Senha é obrigatório")]
	[StringLength(100, MinimumLength = 8, ErrorMessage = "Senha não pode exceder 100 caracteres e deve conter, pelo menos, 8 caracteres")]
	public string? Senha
    {
        get { return senha; }
        set { senha = value; }
    }
    [Required(ErrorMessage = "E-mail é obrigatório")]
	[StringLength(100, ErrorMessage = "Email não pode exceder 100 caracteres")]
	public string? Email
    {
        get { return email; }
        set { email = value; }
    }
	[Required(ErrorMessage = "Endereço é obrigatório")]
	[StringLength(100, ErrorMessage = "Endereço não pode exceder 100 caracteres")]
	public string? Endereco
    {
        get { return endereco; }
        set { endereco = value; }
    }
	[Required(ErrorMessage = "Telefone é obrigatório")]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "Insira apenas números")]
    [StringLength(11, MinimumLength = 10, ErrorMessage = "O número de telefone só pode ter 11 dígitos no máximo e 10 dígitos no mínimo")]
	public string? Numero
    { 
        get { return numero; } 
        set { numero = value; } 
    }
}
