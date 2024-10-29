using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        public List<Usuario> Usuarios { get; set; }
        public ServiceLogin()
        {
            _passwordHasher = new PasswordHasher<Usuario>();
            Usuarios = new List<Usuario>();
            Usuarios.Add(new()
            {
                Email = "john@wick.com",
                Nome = "John",
                SenhaHash = "AQAAAAIAAYagAAAAEAcwH8ucYtATRdWjLP2Rz6CXgDRW7w6I2q15wZcuyWkPa2QwIEM43l6cCdfwx1edOw==",
                Perfil = new Perfil() { Nome = "Administrador" }
            });
            Usuarios.Add(new()
            {
                Email = "john2@wick.com",
                Nome = "John2",
                SenhaHash = "AQAAAAIAAYagAAAAEAcwH8ucYtATRdWjLP2Rz6CXgDRW7w6I2q15wZcuyWkPa2QwIEM43l6cCdfwx1edOw==",
                Perfil = new Perfil() { Nome = "Cliente" }
            });
            Usuarios.Add(new()
            {
                Email = "john3@wick.com",
                Nome = "John3",
                SenhaHash = "AQAAAAIAAYagAAAAEAcwH8ucYtATRdWjLP2Rz6CXgDRW7w6I2q15wZcuyWkPa2QwIEM43l6cCdfwx1edOw==",
                Perfil = new Perfil() { Nome = "Corretor" }
            });
        }
        public Usuario Autenticar(string email, string senha)
        {
            Usuario usuario = Usuarios.Find(usuario => usuario.Email == email);
            if (usuario != null)
            {
                if (_passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, senha) == PasswordVerificationResult.Success)
                {
                    return usuario;
                }

            }
            throw new AuthenticationExcepion("Dados incorreto!");

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
