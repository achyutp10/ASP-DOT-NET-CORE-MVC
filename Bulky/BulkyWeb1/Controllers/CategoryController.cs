﻿using BulkyWeb1.Data;
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

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name","The DisplayOrder cannot exactly match the Name");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is invalid value");
            }
            if (ModelState.IsValid == true)
            {
                _db.Categories.Add(obj);
                var success = _db.SaveChanges();
                if (success>0)
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
                 TempData["SuccessMessage"] = "Data Not Valid";
                 return View();
            }
            return RedirectToAction("Index","Category");
        }

        public IActionResult Edit(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name","The DisplayOrder cannot exactly match the Name");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is invalid value");
            }
            if (ModelState.IsValid == true)
            {
                _db.Categories.Add(obj);
                var success = _db.SaveChanges();
                if (success>0)
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
                 TempData["SuccessMessage"] = "Data Not Valid";
                 return View();
            }
            return RedirectToAction("Index","Category");
        }
       

    }
}
