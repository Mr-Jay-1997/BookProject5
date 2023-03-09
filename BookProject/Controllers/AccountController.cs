using BookProject.Models;
using DALBookProject.Database;
using DALBookProject.Database.Tables;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BookProject.Controllers
{
    public class AccountController : Controller
    {
        
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
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BookDbContext(BookDbContext.ops.dbOptions))
                {
                    var data= db.Users.Where(e=>e.email == model.Email).SingleOrDefault();

                    if(data != null)
                    {
                        bool isValid = (data.email == model.Email && data.password == model.Password);
                        if(isValid)
                        {
                            var identity = new ClaimsIdentity(new[]
                            {
                                new Claim(ClaimTypes.Name,model.Email)
                            }, 
                            CookieAuthenticationDefaults.AuthenticationScheme);

                            var principal= new ClaimsPrincipal(identity);
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                            HttpContext.Session.SetString("Email",model.Email);
                            TempData["SuccessMessage"] = "Login Successful";
                            return RedirectToAction("Index","Home");
                          
                        }

                        else
                        {
                            TempData["errorPassword"] = "Invalid Password!";
                            return View(model);
                        }
                    }
                    else
                    {
                        TempData["errorEmail"] = "User not found!";
                        return View(model);
                    }
                }
            }
            else
            {
                return View(model);
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


        [AcceptVerbs("Post", "Get")]
        //[ValidateAntiForgeryToken]
        public IActionResult IsUserExist(string Email)
        {
            using (var db = new BookDbContext(BookDbContext.ops.dbOptions))
            {
                System.Threading.Thread.Sleep(10000);
                var data = db.Users.Where(e => e.email == Email).FirstOrDefault();
                if (data != null)
                {
                    return Json($"Email: {Email} already exists!");
                }
                else
                {
                    return Json(true);
                }   
            }  
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUpViewModel model)
        {
            using (var db = new BookDbContext(BookDbContext.ops.dbOptions))
            {
                
                if (ModelState.IsValid)
                {
                    var data = new USER()
                    {
                        firstname = model.FirstName,
                        lastname = model.LastName,
                        email = model.Email,
                        password = model.Password,
                        mobile = model.Mobile
                    };
                    
                    db.Users.Add(data);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "SignUp Successful";
                    return RedirectToAction("Login");
                    //return View(model);
                }
                else
                {
                    TempData["errorMessage"] = "Empty form cant be submitted!";
                    return View(model);
                }
            }
            
        }
    }
}
