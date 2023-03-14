using BLLBookProject;
using BookProject.Models;
using DALBookProject.Database;
using DALBookProject.Database.Tables;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SharedLibrary.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookProject.Controllers
{
    public class AccountController : Controller
    {
        BLLUser blluser = new BLLUser();
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BookDbContext(BookDbContext.ops.dbOptions))
                {
                    var data = blluser.Login(loginViewModel);
                    if(data != null)
                    {
                        //bool isValid = (data.Email == loginViewModel.Email && data.Password == loginViewModel.Password);
                        var identity = new ClaimsIdentity(new[]
                        {
                           new Claim(ClaimTypes.Name,data.Email)
                        },
                         CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("Email", data.Email);
                        TempData["SuccessMessage"] = "Login Successful";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                       TempData["errorPassword"] = "Invalid Password!";
                       return View(loginViewModel);
                    }
                    
                    //else
                    //{
                    //    TempData["errorEmail"] = "User not found!";
                    //    return View(loginViewModel);
                    //}
                }
            }
            else
            {
                return View(loginViewModel);
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookies = Request.Cookies.Keys;
            foreach(var cookies in storedCookies)
            {
                Response.Cookies.Delete(cookies);
            }
            TempData["SuccessMessage"] = "Logout Successful";
            return RedirectToAction("Login","Account");
        }


        //[AcceptVerbs("Post", "Get")]
        ////[ValidateAntiForgeryToken]
        //public IActionResult IsUserExist(SignUpViewModel signUpViewModel)
        //{
        //    using (var db = new BookDbContext(BookDbContext.ops.dbOptions))
        //    {
        //        //System.Threading.Thread.Sleep(10000);
        //        var data = blluser.IsUserExist(signUpViewModel);
        //        if (data != null)
        //        {
        //            return Json($"Email: {signUpViewModel.Email} already exists!");
        //        }
        //        else
        //        {
        //            return Json(true);
        //        }
        //    }
        //}

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUpViewModel signUpViewModel)
        {
            //using (var db = new BookDbContext(BookDbContext.ops.dbOptions))
            {
                if (ModelState.IsValid)
                {
                    signUpViewModel = blluser.SignUp(signUpViewModel);
                    if(signUpViewModel != null)
                    {
                        TempData["SuccessMessage"] = "SignUp Successful";
                        return RedirectToAction("Login");
                        
                    }
                    //return View(signUpViewModel);
                }
                else
                {
                    TempData["errorMessage"] = "Empty form cant be submitted!";
                    return View(signUpViewModel);
                }
                return View(signUpViewModel);
            }
            
        }
    }
}
