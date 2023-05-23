using CRUDWebAPI.Models;
using CRUDWebAPI.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebAPI.Core.Repositories
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(ApiDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public override async Task<IEnumerable<Driver>> GetAll()
        {
            try
            {
                return await _context.Drivers.Where(x => x.Id < 100).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public async Task<Driver> GetByDriverNb(int driverNb)
        {
            try
            {
                return _context.Drivers.FirstOrDefaultAsync(x => x.DriverNumber == driverNb);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
