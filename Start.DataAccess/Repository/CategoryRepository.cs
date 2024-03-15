using Start.DataAccess.Data;
using Start.DataAccess.Repository.IRepository;
using Start.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Start.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void save()
        {
            _db.SaveChanges();
        }

        public void update(Category Obj)
        {
            _db.Categories.Update(Obj);
        }
    }
}
