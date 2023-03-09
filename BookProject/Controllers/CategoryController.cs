using BLLBookProject;
using DALBookProject.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Models;

namespace BookProject.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        BLLCategory bllcategory = new BLLCategory();
        public IActionResult Index()
        {
            var categoryList = bllcategory.GetCategoryList();
            return View(categoryList);
        }

        public IActionResult Details(int id)
        {
            var category = bllcategory.GetCategory(id);
            return View(category);
        }

        public IActionResult Create()
        {
            var categoryModel = new CategoryModel();
            return PartialView("_CreatePartialView",categoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel categoryModel)
        {
            bool check = false;
            string ErrMessage = "";
            try
            {
                if (ModelState.IsValid)
                {

                    if (bllcategory.IsCategoryExists(categoryModel.CategoryName) == true)
                    {
                        ErrMessage = ErrMessage + " Category " + categoryModel.CategoryName + " already Exists!!!";
                    }

                    if (ErrMessage == "")
                    {
                        categoryModel = bllcategory.CreateCategory(categoryModel);
                        check = true;
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Value cannot be empty";
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Category " + categoryModel.CategoryName + " Failed to Create!!!";
            }
            if (check == false)
            {
                TempData["ErrorMessage"] = ErrMessage;
                ModelState.AddModelError("", ErrMessage);
                //return PartialView("_CreatePartialView", categoryModel);
            }
            else
            {
                TempData["SuccessMessage"] = "Category " + categoryModel.CategoryName + " Created Successfully!!!";

            }
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int id)
        {
            var updatedCategory = new CategoryModel();
            updatedCategory = bllcategory.GetCategory(id);
            
            return PartialView("_EditPartialView",updatedCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int categoryId, CategoryModel updatedCategory)
        {
            bool check = false;
            string ErrMessage = "";
            try
            {
                if(bllcategory.IsCategoryExists(updatedCategory.CategoryName,updatedCategory.CategoryId) == true)
                {
                    ErrMessage = "Category " + updatedCategory.CategoryName + " already Exists!!!";
                }
                if(ErrMessage=="")
                {
                    updatedCategory = bllcategory.UpdateCategory(categoryId, updatedCategory);
                    TempData["SuccessMessage"] = "Category " + updatedCategory.CategoryName + " Saved Successfully";
                    check = true;   
                }

            }
            catch(Exception ex)
            {
                ErrMessage = ErrMessage + "" + ex.Message;
                TempData["ErrorMessage"] = "Category " + updatedCategory.CategoryName + " not Updated";
            }
           
            if (check == false)
            {
                TempData["ErrorMessage"] = ErrMessage;
                ModelState.AddModelError("", ErrMessage);
                //return PartialView("_CreatePartialView", categoryModel);
            }
            else
            {
                
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var categoryModel = new CategoryModel();
            categoryModel = bllcategory.GetCategory(id);
           
            return PartialView("_DeletePartialView",categoryModel);
        }
        [HttpPost]
        public IActionResult Delete(CategoryModel categoryModel)
        {
            try
            {
                //ModelState.Remove("CategoryName");
                if(ModelState.IsValid)
                {
                    var result = bllcategory.DeleteCategory(categoryModel);

                    if(result== true)
                    {
                        TempData["SuccessMessage"] = "Category " + categoryModel.CategoryName + " Deleted Successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Category " + categoryModel.CategoryName + " failed to Delete";
                    }
                }
                else
                {
                    TempData["error"] = "please check fields";
                }
            
            }
            catch
            {
                // Handle any exceptions
            }
            return RedirectToAction(nameof(Index));
        }

        //[AcceptVerbs("Post", "Get")]
        //public IActionResult IsCategoryExist(string CategoryName)
        //{
        //    using (var db = new BookDbContext(BookDbContext.ops.dbOptions))
        //    {
        //        var data = db.Categories.Where(e => e.categoryname == CategoryName).SingleOrDefault();
        //        if (data != null)
        //        {
        //            return Json($"Category: {CategoryName} already exists!");
        //        }
        //        else
        //        {
        //            return Json(true);
        //        }

        //    }
        //}
    }
}
