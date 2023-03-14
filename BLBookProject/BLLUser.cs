using BookProject.Models;
using DALBookProject.Database;
using DALBookProject.Database.Tables;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLBookProject
{
    public class BLLUser
    {
        DALUser daluser = new DALUser();
        public LoginViewModel Login(LoginViewModel loginViewModel)
        {
            try
            {
                //UserModel userModel = new UserModel();
                var result = daluser.Login(loginViewModel); 
                return result;
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
                var result = daluser.SignUp(signUpViewModel);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        //public SignUpViewModel IsUserExist(SignUpViewModel signUpViewModel)
        //{
        //    var result = daluser.IsUserExist(signUpViewModel);
        //    //if (result !=null)
        //    //{
        //        return result;
        //    //}
        //    //return signUpViewModel;
        //}
    }
}
