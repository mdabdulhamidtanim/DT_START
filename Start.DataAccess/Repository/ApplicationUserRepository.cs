using Start.DataAccess.Repository;
using Start.DataAccess.Data;
using Start.DataAccess.Repository.IRepository;
using Start.Models;

namespace Start.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}