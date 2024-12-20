﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Imobiliaria.Dominio.ModuloImovel;
using Imobiliarias.Models;
using Imobiliaria.Dominio.ModuloCliente;
using Imobiliaria.Dominio.ModuloCorretor;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Imobiliarias.Controllers
{
	[Authorize]
	public class ImoveisController : Controller
    {
        public IServiceImovel serviceImovel { get; }
        public IServiceCliente serviceCLiente { get; }
        public IServiceCorretor serviceCorretor { get; }
        public ImoveisController(IServiceImovel contextImovel, IServiceCorretor contextCorretor, IServiceCliente contextCliente )
        {
            serviceImovel = contextImovel;
            serviceCorretor = contextCorretor;
            serviceCLiente = contextCliente;
        }

        // GET: Imoveis
        public async Task<IActionResult> Index()
        {
            List<Imovel> imovelVO = serviceImovel.TrazerImoveis();
            return View(imovelVO.ToImoveisViewModelList());
        }

        // GET: Imoveis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var imovel = serviceImovel.TragaImovelId(id.Value);
            ImovelViewModel imovelViewModel = imovel.ToImovelViewModel();


            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovelViewModel);
        }

        // GET: Imoveis/Create
        public IActionResult Create()
        {
            ViewData["ClienteDonoId"] = new SelectList(serviceCLiente.TrazerClientes(), "ClienteId", "Cpf");
            ViewData["CorretorGestorId"] = new SelectList(serviceCorretor.TrazerCorretores(), "CorretorId", "Cpf");
            ViewData["CorretorNegocioId"] = new SelectList(serviceCorretor.TrazerCorretores(), "CorretorId", "Cpf");
            return View();
        }

        // POST: Imoveis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateImovelViewModel imovel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    
                    var imovelId = serviceImovel.CriarImovel(imovel.ToimovelModel());
                    serviceImovel.UploadImg(imovelId, imovel.ArquivosFotos);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(imovel);
        }

        // GET: Imoveis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["ClienteDonoId"] = new SelectList(serviceCLiente.TrazerClientes(), "ClienteId", "Cpf");
            ViewData["CorretorGestorId"] = new SelectList(serviceCorretor.TrazerCorretores(), "CorretorId", "Cpf");
            ViewData["CorretorNegocioId"] = new SelectList(serviceCorretor.TrazerCorretores(), "CorretorId", "Cpf");
            var imovel = serviceImovel.TragaImovelId(id.Value);
            EditarImovelViewModel editarImovelViewModel = imovel.ToImovelViewModelEditar();

            if (imovel == null)
            {
                return NotFound();
            }
            return View(editarImovelViewModel);
        }

        // POST: Imoveis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarImovelViewModel model, List<string> FotosRemover)
        {

			if (ModelState.IsValid)
			{

				try
				{

					serviceImovel.SalvarImovel(model.ToImovelModel());

					serviceImovel.UploadImg(model.ImovelId, model.ArquivosFotos);

					serviceImovel.RemoveImg(model.ImovelId, FotosRemover);

					return RedirectToAction(nameof(Index));

				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
                }

            }
            return View(model);

        }
    

        // GET: Imoveis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var imovel = serviceImovel.TragaImovelId(id.Value);
            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel.ToImovelViewModel());
        }

        // POST: Imoveis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            serviceImovel.RemoverImovel(id);


            return RedirectToAction(nameof(Index));

        }

      
    }
}
