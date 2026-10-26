using ControleFinanceiro.Data;
using ControleFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Controllers
{
    public class ContasController : Controller
    {
        private readonly FinancasContext _context;
        public ContasController(FinancasContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contas = await _context.Contas.Include(c => c.Usuario).ToListAsync();
            return View(contas);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var conta = await _context.Contas.Include(c => c.Usuario).FirstOrDefaultAsync(c => c.Id == id);
            if(conta == null)
            {
                return NotFound();
            }
            return View(conta);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList(_context.Usuarios, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Conta conta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.UsuarioId = new SelectList(_context.Usuarios, "Id", "Nome", conta.UsuarioId);
            return View(conta);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var conta = await _context.Contas.FindAsync(id);
            if (conta == null)
            {
                return NotFound();
            }
            ViewBag.UsuarioId = new SelectList(_context.Usuarios, "Id", "Nome", conta.UsuarioId);
            return View(conta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Conta conta)
        {
            if (ModelState.IsValid)
            {
                _context.Update(conta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            if(conta == null)
            {
                return NotFound();
            }
            return View(conta);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var conta = await _context.Contas.Include(c => c.Usuario).FirstOrDefaultAsync(c => c.Id == id);
            if (conta == null)
            {
                return NotFound();
            }
            return View(conta);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conta = await _context.Contas.FindAsync(id);
            if (conta == null)
            {
                return NotFound();
            }
            _context.Contas.Remove(conta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
