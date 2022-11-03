using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFilmes.Models;

namespace MvcFilmes.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        
        //Completar o Migrations: Criar Banco/Tabela
        public IActionResult RunMigrate()
        {
            try
            {
                _context.Database.Migrate();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                throw;
            }
        }

        public IActionResult Index()
        {
            try
            {
                var filmes = _context.Filmes.ToList();
                return View(filmes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Filme f)
        {
            if (ModelState.IsValid)
            {
                var filme = new Filme()
                {
                    Nome = f.Nome,
                    Atores = f.Atores
                };

                _context.Add(f);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var filme = _context.Filmes.Where(a => a.Id == id).FirstOrDefault();

            if (filme is null)
                return NotFound();

            return View(filme);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Filme f)
        {
            if (ModelState.IsValid)
            {
                _context.Update(f);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(f);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var filme = _context.Filmes.Where(a => a.Id == id).FirstOrDefault();

            if (filme is not null)
            {
                _context.Remove(filme);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(filme);
        }
    }
}