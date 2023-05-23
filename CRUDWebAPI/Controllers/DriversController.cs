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

        private readonly ApiDbContext _context;
        public DriversController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_context.Drivers.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //var driver = _drivers.FirstOrDefault(d => d.Id == id);
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id);

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
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Put(int id, Driver driver)
        {
            if (id != driver.Id)
            {
                return BadRequest();
            }
            var existingDriver = _context.Drivers.FirstOrDefault(d => d.Id == id);
            if (existingDriver == null)
            {
                return NotFound();
            }
            existingDriver.Name = driver.Name;
            existingDriver.DriverNumber = driver.DriverNumber;
            existingDriver.Team = driver.Team;
            return Ok(existingDriver);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id);
            if (driver == null)
            {
                return NotFound();
            }
            _context.Drivers.Remove(driver);
            return Ok(driver);
        }

    }
}
