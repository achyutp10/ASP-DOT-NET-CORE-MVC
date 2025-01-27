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

        //[HttpPost]
        //public IActionResult Create(Category obj)
        //{
        //    if (ModelState.IsValid == true)
        //    {
        //        _db.Categories.Add(obj);
        //        var success = _db.SaveChanges();
        //        if (success>0)
        //        {
        //            TempData["SuccessMessage"] = "Data Inserted Successfull";
        //        }
        //        else
        //        {
        //            TempData["SuccessMessage"] = "Data Not Inserted";
        //        }
        //    }
        //    else
        //    {
        //         TempData["SuccessMessage"] = "Data Not Valid";
        //         return View();
        //    }
        //    return RedirectToAction("Index","Category");
        //}

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid == true)
            {
                _db.Categories.Add(obj);
                var success = _db.SaveChanges();
                if (success > 0)
                {
                    TempData["SuccessMessage"] = "Data Inserted Successfull";
                }
                else
                {
                    TempData["SuccessMessage"] = "Data Not Inserted";
                }
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index", "Category");
        }
    }
}
