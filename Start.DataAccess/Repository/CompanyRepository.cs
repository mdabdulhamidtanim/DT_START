using Start.DataAccess.Repository.IRepository;
using Start.DataAccess.Data;
using Start.Models;
using Start.DataAccess.Repository;

namespace Start.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

       

        

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}