using ASS_PRC.Data.Entity;
using ASS_PRC.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASS_PRC.Services.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Category>> getCategory()
        {
            var category = _unitOfWork.Repository<Data.Entity.Category>().GetAll().AsQueryable();
            List<Data.Entity.Category> listCategory = new List<Data.Entity.Category>();
            foreach (var item in category)
            {
                Data.Entity.Category categoryObj = new Data.Entity.Category()
                {
                    Id = item.Id,
                    CategoryName = item.CategoryName
                };
                listCategory.Add(categoryObj);
            }
            return listCategory;
        }
    }
}
