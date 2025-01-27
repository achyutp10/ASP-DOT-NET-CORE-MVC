using BulkyWeb1.Data;
using BulkyWeb1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCList = _db.Categories.ToList();
            return View(objCList);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
