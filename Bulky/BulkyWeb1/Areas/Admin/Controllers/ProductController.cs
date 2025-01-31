using System.Collections.Generic;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objCList = _unitOfWork.Product.GetAll().ToList();
            
            return View(objCList);
        }

        public IActionResult Upsert(int? id) // UpdateInsert
        {
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem{
            //    Text = u.Name,Value = u.Id.ToString()});
            ////ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //Create
                return View(productVM);
            }
            else
            {
                // Update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (obj.Product.Title != null && obj.Product.Title.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is invalid value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj.Product);
                TempData["SuccessMessage"] = "Data Created";

                _unitOfWork.Save();
            }
            else
            {
                obj.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                TempData["ErrorMessage"] = "Data Not Valid";
                return View(obj);
            }
            return RedirectToAction("Index", "Product");
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //    //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //    //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{

        //    if (ModelState.IsValid == true)
        //    {
        //        _unitOfWork.Product.Update(obj);
        //        TempData["SuccessMessage"] = "Data Updated";
        //        _unitOfWork.Save();
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = "Data Not Valid";
        //        return View();
        //    }
        //    return RedirectToAction("Index", "Product");
        //}


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            TempData["SuccessMessage"] = "Data Deleted";

            _unitOfWork.Save();
            return RedirectToAction("Index", "Product");
        }
    }
}
