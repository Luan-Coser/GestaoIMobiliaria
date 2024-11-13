using Imobiliaria.Dominio.ModuloUsuario;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Imobiliarias.Models
{
    public class UsuarioViewModel
    {
        [Required]
        public string Nome { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Senha { get; set; } = null!;

        public int? ClienteId { get; set; }

        public int? CorretorId { get; set; }
        public int? PerfilId { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
	public static class UsuarioViewModelExtensions
	{
		public static UsuarioViewModel ToUsuarioViewModel(this Usuario usuario)
        {
			UsuarioViewModel usuarioViewModel = new UsuarioViewModel();
			usuarioViewModel.Nome = usuario.Nome;
			usuarioViewModel.Email = usuario.Email;
            usuarioViewModel.Senha = usuario.SenhaHash;
            usuarioViewModel.ClienteId = usuario.ClienteId;
			usuarioViewModel.CorretorId = usuario.CorretorId;
			usuarioViewModel.PerfilId = usuario.PerfilId;
			usuarioViewModel.UsuarioId = usuario.UsuarioId;
			usuarioViewModel.DataCriacao = usuario.DataCriacao;

            return usuarioViewModel;
		}

		public static List<UsuarioViewModel> ToUsuariosViewModelList(this List<Usuario> usuarios)
		{
			List<UsuarioViewModel> usuariosViewModels = new List<UsuarioViewModel>();
			foreach (Usuario usuario in usuarios)
			{
				UsuarioViewModel usuarioViewModel = usuario.ToUsuarioViewModel();
				usuariosViewModels.Add(usuarioViewModel);
			}
			return usuariosViewModels;
		}

        public static Usuario ToUsuario(this UsuarioViewModel createUsuarioViewModel)
        {
            // Mapeamento para a entidade Usuario
            var user = new Usuario
            {
                Nome = createUsuarioViewModel.Nome,
                Email = createUsuarioViewModel.Email,
                SenhaHash = createUsuarioViewModel.Senha,
                DataCriacao = createUsuarioViewModel.DataCriacao,
                PerfilId = createUsuarioViewModel.ClienteId.HasValue ? 1 : (createUsuarioViewModel.CorretorId.HasValue ? 2 : 3) // 1: Cliente, 2: Corretor, 3: Administrador,

            };

            user.ClienteId = createUsuarioViewModel.ClienteId.HasValue
                ? createUsuarioViewModel.ClienteId.Value
                : user.ClienteId;

            user.CorretorId = createUsuarioViewModel.CorretorId.HasValue
                ? createUsuarioViewModel.CorretorId.Value
                : user.CorretorId;

            user.UsuarioId = createUsuarioViewModel.UsuarioId.HasValue
                ? createUsuarioViewModel.UsuarioId.Value
                : user.UsuarioId;

            return user;
        }

 
    }

}
