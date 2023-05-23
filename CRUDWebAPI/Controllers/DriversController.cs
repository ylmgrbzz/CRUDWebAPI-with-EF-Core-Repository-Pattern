using CRUDWebAPI.Core;
using CRUDWebAPI.Data;
using CRUDWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase

    {
        //private static List<Driver> _drivers = new List<Driver>()
        //{
        //    new Driver() { Id = 1, Name = "Lewis Hamilton", DriverNumber = 44, Team = "Mercedes" },
        //    new Driver() { Id = 2, Name = "Valtteri Bottas", DriverNumber = 77, Team = "Mercedes" },
        //    new Driver() { Id = 3, Name = "Max Verstappen", DriverNumber = 33, Team = "Red Bull Racing" },
        //    new Driver() { Id = 4, Name = "Sergio Perez", DriverNumber = 11, Team = "Red Bull Racing" },
        //    new Driver() { Id = 5, Name = "Lando Norris", DriverNumber = 4, Team = "McLaren" },
        //    new Driver() { Id = 6, Name = "Daniel Ricciardo", DriverNumber = 3, Team = "McLaren" },
        //};

        //private readonly ApiDbContext _context;
        //public DriversController(ApiDbContext context)
        //{
        //    _context = context;
        //}

        private readonly IUnıtOfWork _unıtOfWork;
        public DriversController(UnıtOfWork unıtOfWork)
        {
            _unıtOfWork = unıtOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return Ok(_context.Drivers.ToListAsync());
            return Ok(await _unıtOfWork.Drivers.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //var driver = _drivers.FirstOrDefault(d => d.Id == id);
            //var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id);

            var driver = await _unıtOfWork.Drivers.GetById(id);

            if (driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Post(Driver driver)
        {
            //_drivers.Add(driver);
            //return CreatedAtAction(nameof(Get), new { id = driver.Id }, driver);
            //_context.Drivers.Add(driver);
            //await _context.SaveChangesAsync();
            await _unıtOfWork.Drivers.Add(driver);
            await _unıtOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPatch]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateDriver(Driver driver)
        {
            //var driverToUpdate = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == driver.Id);
            var driverToUpdate = await _unıtOfWork.Drivers.GetById(driver.Id);
            if (driverToUpdate == null)
            {
                return NotFound();
            }
            driverToUpdate.Name = driver.Name;
            driverToUpdate.DriverNumber = driver.DriverNumber;
            driverToUpdate.Team = driver.Team;
            //await _context.SaveChangesAsync();
            await _unıtOfWork.CompleteAsync();
            return Ok(driverToUpdate);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id);
            var driver = await _unıtOfWork.Drivers.GetById(id);
            if (driver == null)
            {
                return NotFound();
            }
            //_context.Drivers.Remove(driver);
            await _unıtOfWork.Drivers.Delete(driver);
            await _unıtOfWork.CompleteAsync();
            return Ok(driver);
        }
    }
}
