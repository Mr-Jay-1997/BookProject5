using BLLBookProject;
using BookProject.Models;
using DALBookProject.Database;
using DALBookProject.Database.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SharedLibrary.Models;
using System.Drawing.Design;
using System.Globalization;
using System.Net.WebSockets;


namespace BookProject.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        BLLBook bllbook = new BLLBook();
        public IActionResult Index(int? catId = null)
        {
            var bookList = bllbook.GetBookList(catId);
            ViewBag.Categories = GetCategories();
            //if(category.HasValue)
            //{
            //    var books = bllbook.GetBookList().Where(b=>b.CategoryId==id);
            //}
            return View(bookList);
        }

        public IActionResult Details(int id)
        {
            var book = bllbook.GetBook(id);
            return PartialView("_DetailsPartialView", book);
        }

        //public IActionResult Create()
        //{
        //    using (var db = new BookDbContext(BookDbContext.ops.dbOptions))
        //    {
        //        var bookModel = new BookModel();
        //        //BLLCategory bLLCategory = new BLLCategory();
        //        ViewBag.Categories = GetCategories();

        //        return View(bookModel);
        //    }
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public IActionResult Create(BookModel bookModel)
        //{
        //    bool check = false;
        //    string ErrMessage = "";
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (bllbook.IsBookExists(bookModel.BookName) == true)
        //            {
        //                ErrMessage = ErrMessage + " Book " + bookModel.BookName + " already Exists!!!";
        //            }
        //            if (ErrMessage == "")
        //            {
        //                bookModel = bllbook.CreateBook(bookModel);
        //                check = true;
        //            }
        //        }
        //        else
        //        {
        //            TempData["ErrorMessage"] = "Value cannot be empty";
        //        }
        //    }
        //    catch
        //    {
        //        TempData["ErrorMessage"] = "Book " + bookModel.BookName + " failed to create";
        //    }
        //    if(check == false)
        //    {
        //        TempData["ErrorMessage"] = ErrMessage;
        //        ModelState.AddModelError("", ErrMessage);
        //        //return PartialView("_CreatePartialView", categoryModel);
        //    }
        //    else
        //    {
        //        TempData["SuccessMessage"] = "Book " + bookModel.BookName + " Created Successfully";  
        //    }
        //    return RedirectToAction(nameof(Index));
        //    //return View(bookModel);

        //}

        //public IActionResult Edit(int id)
        //{
        //    //BLLCategory bLLCategory = new BLLCategory();

        //    BookModel updatedBook = new BookModel();
        //    updatedBook = bllbook.GetBook(id);
        //    ViewBag.Categories = GetCategories();
        //    ViewBag.Categories = GetCategories();
        //   // updatedBook.Selected = updatedBook.CategoryId;
        //    return View(updatedBook);
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public IActionResult Edit(int bookId, BookModel updatedBook)
        //{
        //    bool check = false;
        //    string ErrMessage = "";
        //    ViewBag.Categories = GetCategories();
        //    try
        //    {
        //        if (bllbook.IsBookExists(updatedBook.BookName, updatedBook.BookId) == true)
        //        {
        //            ErrMessage = "Book " + updatedBook.BookName + " already Exists!!!";
        //        }
        //        if (ErrMessage == "")
        //        {
        //            updatedBook = bllbook.UpdateBook(bookId, updatedBook);
        //            TempData["SuccessMessage"] = "Book " + updatedBook.BookName + " Saved Successfully";
        //            check= true;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        ErrMessage = ErrMessage + "" + ex.Message;
        //        TempData["ErrorMessage"] = "Book " + updatedBook.BookName + " not Updated";
        //    }
        //    if (check == false)
        //    {
        //        TempData["ErrorMessage"] = ErrMessage;
        //        ModelState.AddModelError("", ErrMessage);
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpGet]
        public IActionResult CreateOrUpdateBook(int? id)
        {
            
            ViewBag.Categories = GetCategories();
            // If an id is provided, retrieve the book by id
            if (id.HasValue)
            {
                var book = bllbook.GetBook(id.Value);
                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }

            // If no id is provided, create a new book model
            var newBook = new BookModel();
            return View(newBook);
        }


        [HttpPost]
        public IActionResult CreateOrUpdateBook(BookModel bookModel)
        {
            bool check = false;
            string ErrMessage = "";
            ViewBag.Categories = GetCategories();
            if (ModelState.IsValid)
            {
                try
                {
                    if (bllbook.IsBookExists(bookModel.BookName) == true)
                    {
                        ErrMessage = "Book " + bookModel.BookName + " already Exists!!!";
                    }
                    if (ErrMessage == "")
                    {
                        var result = bllbook.CreateOrUpdateBook(bookModel);
                        if (result != null)
                        {
                            TempData["SuccessMessage"] = "Book " + bookModel.BookName + " saved Succesfully";
                            check= true;
                            //return RedirectToAction("Index", "Book");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Book " + bookModel.BookName + " failed to save";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrMessage = ErrMessage + "" + ex.Message;
                    TempData["ErrorMessage"] = "Book " + bookModel.BookName + " not saved";
                }
                if (check == false)
                {
                TempData["ErrorMessage"] = ErrMessage;
                ModelState.AddModelError("", ErrMessage);
                }
                return RedirectToAction(nameof(Index));

            }
            return View(bookModel);
        }



        public IActionResult Delete(int id)
        {
            var bookModel = new BookModel();
            bookModel = bllbook.GetBook(id);
            
            return PartialView("_DeletePartialView", bookModel);
        }

        [HttpPost]
        public IActionResult Delete(BookModel bookModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = bllbook.DeleteBook(bookModel);
                    if (result==true)
                    {
                        TempData["SuccessMessage"] = "Book " + bookModel.BookName + " Deleted Successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Book " + bookModel.BookName + " Delete Failed";
                    }

                }
                else
                {
                    foreach(var key in ModelState.Keys)
                    {
                        foreach(var error in ModelState[key].Errors)
                        {
                            var errorMessage = error.ErrorMessage;
                        }
                    }
                    //TempData["ErrorMessage"] = "Book " + bookModel.BookName + " Delete Failed";
                }
                
            }
            catch
            {
                // Handle any exceptions
            }
            return RedirectToAction(nameof(Index));
        }

        private List<SelectListItem> GetCategories()
        {
            var lstcategories = new List<SelectListItem>();
            BLLCategory bLLCategory= new BLLCategory();
            List<CategoryModel> categories = bLLCategory.GetCategoryList();
            lstcategories = categories.Select(ut => new SelectListItem()
            {
                Value = ut.CategoryId.ToString(),
                Text = ut.CategoryName,
                //Selected= ut.CategoryId == 3
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = null,
                Text = "---Select Category---"
                 
            };
            var allItem = new SelectListItem()
            {
                Value = null,
                Text = "All Categories"
            };
            
            lstcategories.Insert(0, defItem);
            lstcategories.Insert(1, allItem);
            return lstcategories;
        }
    }
}
