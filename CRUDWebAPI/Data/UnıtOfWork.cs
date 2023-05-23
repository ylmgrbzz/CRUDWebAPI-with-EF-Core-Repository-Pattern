using CRUDWebAPI.Core;
using CRUDWebAPI.Core.Repositories;

namespace CRUDWebAPI.Data
{
    public class UnıtOfWork : IUnıtOfWork, IDisposable
    {
        private readonly ApiDbContext _context;
        private readonly ILogger _logger;

        public IDriverRepository Drivers { get; private set; }

        public UnıtOfWork(ApiDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            Drivers = new DriverRepository(_context, _logger);
        }
        public Task CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
