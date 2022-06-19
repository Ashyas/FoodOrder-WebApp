using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CategoryRepsitory : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepsitory(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(Category category)
        {
            var objFromDb = _db.Category.FirstOrDefault (c => c.Id == category.Id); ;
            objFromDb.Name = category.Name;
            objFromDb.DisplayOrder = category.DisplayOrder;
        }
    }
}
