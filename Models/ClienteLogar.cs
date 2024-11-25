using System.ComponentModel.DataAnnotations;
namespace DemeTech.Models
{
	public class ClienteLogar
	{
		private string? cnpj;
		private string? senha;

		//Validação de dados inseridos no login pelo usuário.
		[Required(ErrorMessage = "Cnpj é obrigatório")] //Obriga a inserção para este dado.
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Insira apenas números")] //Define que apenas números são permitidos.
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Cnpj só pode ter 14 dígitos")] //Define o mínimo e o máximo de caracteres permitidos.
		public string? Cnpj
		{
			get { return cnpj; }
			set { cnpj = value; }
		}
		[Required(ErrorMessage = "Senha é obrigatório")]
		[StringLength(100, ErrorMessage = "Senha não pode exceder 100 caracteres")]
		public string? Senha
		{
			get { return senha; }
			set { senha = value; }
		}

	}
}
