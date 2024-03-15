using Start.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Start.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
     void update(Category Obj);
        void save();
    }
}
