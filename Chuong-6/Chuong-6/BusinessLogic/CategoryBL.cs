using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chuong_6.DataAccess;

namespace Chuong_6.BusinessLogic
{
    public class CategoryBL
    {
        private CategoryDA categoryDA = new CategoryDA();

        public List<Category> GetAll()
        {
            return categoryDA.GetAll();
        }

        public Category GetByID(int id)
        {
            return categoryDA.GetByID(id);
        }

        public int Insert(Category category)
        {
            // Có thể thêm validation ở đây
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException("Tên danh mục không được để trống");
            }

            if (category.Type != 1 && category.Type != 2)
            {
                throw new ArgumentException("Loại danh mục phải là 1 (đồ ăn) hoặc 2 (thức uống)");
            }

            return categoryDA.Insert(category);
        }

        public int Update(Category category)
        {
            // Có thể thêm validation ở đây
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException("Tên danh mục không được để trống");
            }

            if (category.Type != 1 && category.Type != 2)
            {
                throw new ArgumentException("Loại danh mục phải là 1 (đồ ăn) hoặc 2 (thức uống)");
            }

            return categoryDA.Update(category);
        }

        public int Delete(int id)
        {
            return categoryDA.Delete(id);
        }

        public List<Category> Find(string keyword)
        {
            List<Category> allCategories = categoryDA.GetAll();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return allCategories;
            }

            List<Category> result = new List<Category>();
            string lowerKeyword = keyword.ToLower();

            foreach (Category category in allCategories)
            {
                if (category.Name.ToLower().Contains(lowerKeyword))
                {
                    result.Add(category);
                }
            }

            return result;
        }
    }
}
