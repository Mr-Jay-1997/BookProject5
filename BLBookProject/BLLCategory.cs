using DALBookProject.Database;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLBookProject
{
    public class BLLCategory
    {
        DALCategory dalcategory = new DALCategory();
        public List<CategoryModel> GetCategoryList()
        {
            return dalcategory.GetCategoryList();
        }

        public CategoryModel GetCategory(int id)
        {
            return dalcategory.GetCategory(id);
        }

        public CategoryModel CreateCategory(CategoryModel categoryModel)
        {
            return dalcategory.CreateCategory(categoryModel);
        }

        public CategoryModel UpdateCategory(int categoryId, CategoryModel updatedCategory)
        {
            return dalcategory.UpdateCategory(categoryId, updatedCategory);
        }


        public Boolean DeleteCategory(CategoryModel categoryModel)
        {
            
            var result= dalcategory.DeleteCategory(categoryModel);
            return result;
           
        }

        public Boolean IsCategoryExists(string categoryName)
        {
            var result= dalcategory.IsCategoryExists(categoryName);
            return result;
        }

        public Boolean IsCategoryExists(string categoryName, int id)
        {
            var result = dalcategory.IsCategoryExists(categoryName,id);
            return result;
        }
    }
}
