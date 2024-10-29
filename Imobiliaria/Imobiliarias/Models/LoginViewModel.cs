using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Imobiliarias.Models

{
	public class LoginViewModel
    {
		[EmailAddress(ErrorMessage = "Email obrigatorio")]
		public string Email { get; set; }
		[Required]
		[MaxLength(16)]
		[MinLength(6)]
		public string Senha { get; set; }

    }

}
