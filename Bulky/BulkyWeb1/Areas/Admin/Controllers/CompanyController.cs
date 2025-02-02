using System.Collections.Generic;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> objCList = _unitOfWork.Company.GetAll().ToList();
            
            return View(objCList);
        }

        public IActionResult Upsert(int? id) // UpdateInsert
        {
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem{
            //    Text = u.Name,Value = u.Id.ToString()});
            ////ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;

            
            if (id == null || id == 0)
            {
                //Create
                return View(new Company());
            }
            else
            {
                // Update
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company CompanyObj, IFormFile? file)
        {
            if (CompanyObj.Name != null && CompanyObj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is invalid value");
            }
            if (ModelState.IsValid)
            {
                if (CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                    TempData["SuccessMessage"] = "Data Created";

                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                    TempData["SuccessMessage"] = "Data Updated";

                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(CompanyObj);
            }
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
        //    //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //    //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
        //    if (CompanyFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(CompanyFromDb);
        //}

        //[HttpPost]
        //public IActionResult Edit(Company obj)
        //{

        //    if (ModelState.IsValid == true)
        //    {
        //        _unitOfWork.Company.Update(obj);
        //        TempData["SuccessMessage"] = "Data Updated";
        //        _unitOfWork.Save();
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = "Data Not Valid";
        //        return View();
        //    }
        //    return RedirectToAction("Index", "Company");
        //}


        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);

        //    if (CompanyFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(CompanyFromDb);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    Company? obj = _unitOfWork.Company.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Company.Remove(obj);
        //    TempData["SuccessMessage"] = "Data Deleted";

        //    _unitOfWork.Save();
        //    return RedirectToAction("Index", "Company");
        //}

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {  
            List<Company> objCList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u=>u.Id==id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted successfully" });

        }


        #endregion
    }
}
