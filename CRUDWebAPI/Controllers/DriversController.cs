using CRUDWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private static List<Driver> _drivers = new List<Driver>()
        {
            new Driver() { Id = 1, Name = "Lewis Hamilton", DriverNumber = 44, Team = "Mercedes" },
            new Driver() { Id = 2, Name = "Valtteri Bottas", DriverNumber = 77, Team = "Mercedes" },
            new Driver() { Id = 3, Name = "Max Verstappen", DriverNumber = 33, Team = "Red Bull Racing" },
            new Driver() { Id = 4, Name = "Sergio Perez", DriverNumber = 11, Team = "Red Bull Racing" },
            new Driver() { Id = 5, Name = "Lando Norris", DriverNumber = 4, Team = "McLaren" },
            new Driver() { Id = 6, Name = "Daniel Ricciardo", DriverNumber = 3, Team = "McLaren" },
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_drivers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var driver = _drivers.FirstOrDefault(d => d.Id == id);
            if (driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Post(Driver driver)
        {
            _drivers.Add(driver);
            return CreatedAtAction(nameof(Get), new { id = driver.Id }, driver);
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Put(int id, Driver driver)
        {
            if (id != driver.Id)
            {
                return BadRequest();
            }
            var existingDriver = _drivers.FirstOrDefault(d => d.Id == id);
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
        public IActionResult Delete(int id)
        {
            var driver = _drivers.FirstOrDefault(d => d.Id == id);
            if (driver == null)
            {
                return NotFound();
            }
            _drivers.Remove(driver);
            return Ok(driver);
        }

    }
}
