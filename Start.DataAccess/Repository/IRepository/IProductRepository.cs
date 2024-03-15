using Start.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Start.DataAccess.Repository.IRepository
{
    public interface IProductRepository:IRepository<Product>
    {
     void update(Product Obj);
        void save();
    }
}
