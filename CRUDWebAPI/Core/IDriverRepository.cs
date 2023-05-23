using CRUDWebAPI.Models;

namespace CRUDWebAPI.Core
{
    public interface IDriverRepository : IGenericRepository<Driver>
    {
        Task<Driver> GetByDriverNb(int driverNb);
    }
}
