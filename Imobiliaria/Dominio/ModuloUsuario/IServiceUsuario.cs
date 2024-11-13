using Imobiliaria.Dominio.ModuloLogin;
using Microsoft.AspNetCore.Identity;
using static Imobiliaria.Dominio.ModuloUsuario.ServiceUsuario;

namespace Imobiliaria.Dominio.ModuloUsuario
{
    public interface IServiceUsuario : IServiceModel<Usuario> { }

    public interface IUsuarioRepositorio : IRepositorio<Usuario> { }

    public partial class ServiceUsuario : IServiceUsuario
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public ServiceUsuario(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _passwordHasher = new PasswordHasher<Usuario>(); ;
        }

        public void Criar(Usuario model)
        {
            //TODO: Fazer hash da senha
            var hashDaSenha = _passwordHasher.HashPassword(model, model.SenhaHash);
            model.SenhaHash = hashDaSenha;

            _usuarioRepositorio.Criar(model);
        }

        public void Remover(int id)
        {
            _usuarioRepositorio.Remover(id);
        }

        public void Salvar(Usuario model)
        {
            _usuarioRepositorio.Salvar(model);
        }

        public Usuario TragaPorId(int id)
        {
            return _usuarioRepositorio.TragaPorId(id);
        }

        public List<Usuario> TragaTodos()
        {
            return _usuarioRepositorio.TragaTodos();
        }
    }

}
