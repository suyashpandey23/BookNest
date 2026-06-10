using Microsoft.AspNetCore.Mvc;
using BookNest.Data;
using BookNest.Models;
namespace BookNest.Controllers;

public class CategoryController : Controller {
    // GET
    private readonly ApplicationDbContext _context;
    public CategoryController(ApplicationDbContext db) {
        _context = db;
    }
    public IActionResult Index() {
        var categories = _context.Categories.ToList();
        return View(categories);
    }
}