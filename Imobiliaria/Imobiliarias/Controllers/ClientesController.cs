using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Imobiliarias;
using Imobiliaria.Dominio.ModuloCliente;
using Imobiliarias.Models;

namespace Imobiliarias.Controllers
{
    public class ClientesController : Controller
    {
        public IServiceCliente ServiceCliente { get; }

        //private readonly ImobiliariaDbContext _context;

        public ClientesController(IServiceCliente serviceCliente)
        {
            ServiceCliente = serviceCliente;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            
            List<Cliente> clientesVO = ServiceCliente.TrazerClientes();
          
            return View(clientesVO.ToClientesViewModelList());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }  
            
            var cliente = ServiceCliente.TragaClienteId(id.Value);
            ClienteViewModel clienteViewModel = cliente.ToClienteViewModel();

            if (cliente == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClienteViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    ServiceCliente.CriarCliente(cliente.ToClienteModel());
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = ServiceCliente.TragaClienteId(id.Value);

            //var cliente = await _context.Clientes.FindAsync(id);
            ClienteViewModel clienteViewModel = cliente.ToClienteViewModel();

            if (cliente == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, ClienteViewModel cliente)
        {
            if (id != cliente.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                
                try
                {
                    ServiceCliente.SalvarCliente(cliente.ToClienteModel());
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = ServiceCliente.TragaClienteId(id.Value);

            //var cliente = await _context.Clientes
            //    .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente.ToClienteViewModel());
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
   
			ServiceCliente.RemoverCliente(id);


			return RedirectToAction(nameof(Index));
        }
    }
}
