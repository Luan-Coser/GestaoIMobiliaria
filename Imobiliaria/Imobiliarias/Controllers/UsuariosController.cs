using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Imobiliarias;
using Imobiliarias.Models;
using Imobiliaria.Dominio.ModuloCliente;
using Imobiliaria.Dominio.ModuloCorretor;
using Imobiliaria.Dominio.ModuloUsuario;

namespace Imobiliarias.Controllers
{
    public class UsuariosController : Controller
    {
		private readonly IServiceUsuario _serviceUsuario;
		private readonly IServiceCliente _serviceCliente;
		private readonly IServiceCorretor _serviceCorretor;
		public UsuariosController(
			IServiceUsuario serviceUsuario,
			IServiceCliente serviceCliente,
			IServiceCorretor serviceCorretor)
		{
			_serviceUsuario = serviceUsuario;
			_serviceCliente = serviceCliente;
			_serviceCorretor = serviceCorretor;
		}
		// GET: Usuarios
		public async Task<IActionResult> Index()
        {
            List<Usuario> UsuarioVO = _serviceUsuario.TragaTodos();
            return View(UsuarioVO.ToUsuariosViewModelList());
        }
 
        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _serviceUsuario.TragaPorId(id.Value);
            UsuarioViewModel usuarioViewModel = usuario.ToUsuarioViewModel();
            
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuarioViewModel);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
			ViewBag.Clientes = _serviceCliente.TrazerClientes().FindAll(x => x.Usuario == null).ToClientesViewModelList();
			ViewBag.Corretores = _serviceCorretor.TrazerCorretores().FindAll(x => x.Usuario == null).ToCorretorViewModelList(); 
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _serviceUsuario.Criar(usuario.ToUsuario());
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.Clientes = _serviceCliente.TrazerClientes().FindAll(x => x.Usuario == null).ToClientesViewModelList();
            ViewBag.Corretores = _serviceCorretor.TrazerCorretores().FindAll(x => x.Usuario == null).ToCorretorViewModelList();
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _serviceUsuario.TragaPorId(id.Value);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioViewModel usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _serviceUsuario.Salvar(usuario.ToUsuario());
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                ModelState.AddModelError("", e.Message);
                }
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _serviceUsuario.TragaPorId(id.Value);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

                _serviceUsuario.Remover(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
