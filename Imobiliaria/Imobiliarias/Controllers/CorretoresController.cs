using Microsoft.AspNetCore.Mvc;
using Imobiliaria.Dominio.ModuloCorretor;
using Imobiliarias.Models;

namespace Imobiliarias.Controllers
{
    public class CorretoresController : Controller
    {
        public IServiceCorretor ServiceCorretor { get; }

        public CorretoresController(IServiceCorretor serviceCorretor)
        {
			ServiceCorretor = serviceCorretor;
        }

        // GET: Corretores
        public async Task<IActionResult> Index()
        {
            List<Corretor> corretorVO = ServiceCorretor.TrazerCorretores();
			return View(corretorVO.ToCorretorViewModelList());

		}

        // GET: Corretores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corretor = ServiceCorretor.TragaCorretorId(id.Value);
            CorretorViewModel corretorViewModel = corretor.ToCorretorViewModel();
			
            if (corretor == null)
            {
                return NotFound();
            }

            return View(corretorViewModel);
        }

        // GET: Corretores/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCorretorViewModel corretor)
        {
			if (ModelState.IsValid)
			{

				try
				{
					ServiceCorretor.CriarCorretor(corretor.ToCorretorModel());
					return RedirectToAction(nameof(Index));

				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}
			return View(corretor);
		}

        // GET: Corretores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var corretor = ServiceCorretor.TragaCorretorId(id.Value);
			CorretorViewModel corretorViewModel = corretor.ToCorretorViewModel();
			if (corretor == null)
            {
                return NotFound();
            }
            return View(corretorViewModel);
        }

        // POST: Corretores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CorretorViewModel corretor)
        {
            if (id != corretor.CorretorId)
            {
                return NotFound();
            }

			if (ModelState.IsValid)
			{


				try
				{
					ServiceCorretor.SalvarCorretor(corretor.ToCorretorModel());
					return RedirectToAction(nameof(Index));

				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
				}

				return RedirectToAction(nameof(Index));
			}
			return View(corretor);
        }

        // GET: Corretores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var corretor = ServiceCorretor.TragaCorretorId(id.Value);
            if (corretor == null)
            {
                return NotFound();
            }

            return View(corretor);
        }

        // POST: Corretores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
			ServiceCorretor.RemoverCorretores(id);


			return RedirectToAction(nameof(Index));

		}
    }
}
