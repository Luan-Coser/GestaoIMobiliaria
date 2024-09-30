using Imobiliaria.Dominio.ModuloCorretor;
using System.ComponentModel.DataAnnotations;

namespace Imobiliarias.Models
{
	public class CorretorViewModel
	{
		public int CorretorId { get; set; }

		public string Nome { get; set; } = null!;

		public string Cpf { get; set; } = null!;

		public string Creci { get; set; } = null!;

		public string? Telefone { get; set; }

		public string? Email { get; set; }
	}
	public class CreateCorretorViewModel
	{
		[Required]
		public string Nome { get; set; } = null!;
		[Required]
		[MaxLength(11, ErrorMessage = "O tamanho maximo é 11!")]
		[MinLength(11, ErrorMessage = "O tamanho minimo é 11!")]
		public string Cpf { get; set; } = null!;
		[Required]
		public string Creci { get; set; }
		public string? Telefone { get; set; }
		[Required]
		[EmailAddress(ErrorMessage = "Email incorreto!")]
		public string? Email { get; set; }
	}
	public static class CorretorViewModelExtensions
	{
		public static CorretorViewModel ToCorretorViewModel(this Corretor corretor)
		{
			CorretorViewModel CorretorViewModel = new CorretorViewModel();
			CorretorViewModel.CorretorId = corretor.CorretorId;
			CorretorViewModel.Nome = corretor.Nome;
			CorretorViewModel.Cpf = corretor.Cpf;
			CorretorViewModel.Email = corretor.Email;
			CorretorViewModel.Telefone = corretor.Telefone;
			CorretorViewModel.Creci = corretor.Creci;
			return CorretorViewModel;
		}
		public static List<CorretorViewModel> ToCorretorViewModelList(this List<Corretor> corretor)
		{
			List<CorretorViewModel> corretoresViewModel = new List<CorretorViewModel>();
			foreach (Corretor Corretor in corretor)
			{
				CorretorViewModel corretorViewModel = Corretor.ToCorretorViewModel();
				corretoresViewModel.Add(corretorViewModel);
			}
			return corretoresViewModel;
		}
		public static Corretor ToCorretorModel(this CreateCorretorViewModel corretorViewModel)
		{
			Corretor corretor = new Corretor();
			corretor.Nome = corretorViewModel.Nome;
			corretor.Cpf = corretorViewModel.Cpf;
			corretor.Email = corretorViewModel.Email;
			corretor.Telefone = corretorViewModel.Telefone;
			corretor.Creci = corretorViewModel.Creci;
			return corretor;
		}
		public static Corretor ToCorretorModel(this CorretorViewModel corretorViewModel)
		{
			Corretor corretor = new Corretor();
			corretor.Nome = corretorViewModel.Nome;
			corretor.Cpf = corretorViewModel.Cpf;
			corretor.Email = corretorViewModel.Email;
			corretor.Telefone = corretorViewModel.Telefone;
			corretor.Creci = corretorViewModel.Creci;
			return corretor;
		}
	}
}
