using BookProject.Models;
using DALBookProject.Database.Tables;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALBookProject.Database
{
    public class DALUser
    {
        public LoginViewModel Login(LoginViewModel loginViewModel)
        {
            try
            {
                using (var db = new BookDbContext(BookDbContext.ops.dbOptions))
                {
                    var data = db.Users.Where(e => e.email == loginViewModel.Email).SingleOrDefault();
                    if (data != null)
                    {
                        bool isValid = (data.email == loginViewModel.Email && data.password == loginViewModel.Password);

                    }
                    return loginViewModel;
                }
            }
            catch
            {
                return null;
            }
        }

        public SignUpViewModel SignUp(SignUpViewModel signUpViewModel)
        {
            try
            {
                using (var db = new BookDbContext(BookDbContext.ops.dbOptions))
                {
                    var data = new USER()
                    {
                        firstname = signUpViewModel.FirstName,
                        lastname = signUpViewModel.LastName,
                        email = signUpViewModel.Email,
                        password = signUpViewModel.Password,
                        mobile = signUpViewModel.Mobile
                    };

                    db.Users.Add(data);
                    db.SaveChanges();
                    
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return signUpViewModel;

            
            
            
        }

        //public SignUpViewModel IsUserExist(SignUpViewModel signUpViewModel)
        //{
        //    using (var db = new BookDbContext(BookDbContext.ops.dbOptions))
        //    {
        //        //System.Threading.Thread.Sleep(10000);
        //        var data = db.Users.Where(e => e.email == signUpViewModel.Email).FirstOrDefault();
        //        return signUpViewModel;
        //    }
        //}
    }
}
