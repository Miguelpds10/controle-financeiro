using ControleFinanceiro.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly FinancasContext _context;

        public UsuariosController(FinancasContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return View(usuarios);
        }
    }
}
