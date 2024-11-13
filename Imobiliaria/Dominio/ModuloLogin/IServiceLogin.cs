using Imobiliaria.Dominio.ModuloUsuario;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Imobiliaria.Dominio.ModuloLogin
{
    public interface IServiceLogin
    {
        Usuario Autenticar(string email, string senha);
    }
    public class ServiceLogin : IServiceLogin
    {
		private readonly IPasswordHasher<Usuario> _passwordHasher;
		private readonly IServiceUsuario _serviceUsuario;
		public ServiceLogin(IServiceUsuario serviceUsuario)
		{
			_serviceUsuario = serviceUsuario;
			_passwordHasher = new PasswordHasher<Usuario>();
		}

		public Usuario Autenticar(string email, string senha)
		{
			Usuario usuario = _serviceUsuario.TragaTodos().Find(user => user.Email == email);

			if (usuario != null)
			{
				if (_passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, senha) ==
					PasswordVerificationResult.Success)
				{
					return usuario;
				}
			}

			throw new AuthenticationException("Dados incorretos");
		}
	}
    [Serializable]
    internal class AuthenticationExcepion : Exception
    {
        public AuthenticationExcepion()
        {
        }

        public AuthenticationExcepion(string? message) : base(message)
        {
        }

        public AuthenticationExcepion(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
