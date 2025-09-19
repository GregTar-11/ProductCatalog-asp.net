using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;



namespace ProductCatalog.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {

        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]// бачать всі
        // GET: /Product/Index
        public IActionResult ProduktIndex()
        {
            var products = _context.Products.ToList(); // берем из базы
            return View(products);
        }

        [AllowAnonymous] // бачать всі
        // GET: /Product/Details/1
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [Authorize(Roles = "Admin")] // доступно тільки Admin
        // GET: /Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(ProduktIndex));
            }
            return View(product);
        }

        // GET: /Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: /Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(ProduktIndex));
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        // GET: /Product/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }


        [Authorize(Roles = "Admin")]
        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(ProduktIndex));
        }
    }
}
