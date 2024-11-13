using Imobiliaria.Dominio.ModuloLogin;
using Imobiliaria.Dominio.ModuloUsuario;
using Imobiliarias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Repositorios.EF.ModuloLogin
{
    class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ImobiliariaDbContext _context;

        public UsuarioRepositorio(ImobiliariaDbContext context)
        {
            _context = context;
        }
        void IRepositorio<Usuario>.Criar(Usuario model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }

        void IRepositorio<Usuario>.Remover(int id)
        {
            var usuarioExistente = _context.Usuarios.FirstOrDefault(c => c.UsuarioId == id);
            if (usuarioExistente != null)
            {
                _context.Entry(usuarioExistente).State = EntityState.Detached;
            }

            _context.Usuarios.Remove(usuarioExistente);
            _context.SaveChanges();
        }

        void IRepositorio<Usuario>.Salvar(Usuario model)
        {
            var usuarioExistente = _context.Usuarios.FirstOrDefault(c => c.UsuarioId == model.UsuarioId);
            if (usuarioExistente != null)
            {
                _context.Entry(usuarioExistente).State = EntityState.Detached;
            }
            _context.Usuarios.Update(model);
            _context.SaveChanges();
        }

        Usuario IRepositorio<Usuario>.TragaPorId(int id)
        {
            var usuarioExistente = _context.Usuarios.FirstOrDefault(c => c.UsuarioId == id);
            return usuarioExistente;
        }

        List<Usuario> IRepositorio<Usuario>.TragaTodos()
        {
            return _context.Usuarios.Include(user => user.Perfil).ToList();
        }
    }
}
