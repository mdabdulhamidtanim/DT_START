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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void save()
        {
            _db.SaveChanges();
        }

        public void update(Product Obj)
        {
            _db.Products.Update(Obj);
        }
    }
}
