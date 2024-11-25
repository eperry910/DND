using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IMongoCollection<Weapon> _WeaponSet;
        private readonly ILogger<WeaponController> _logger;

        public WeaponController(IMongoCollection<Weapon> WeaponSet, ILogger<WeaponController> logger)
        {
            _WeaponSet = WeaponSet;
            _logger = logger;
        }

        [HttpPost(Name = "New Weapon Set")]
        public IActionResult Post([FromBody] Weapon WeaponSet)
        {
            if (WeaponSet == null)
            {
                return BadRequest("Weapon Set cannot be null");
            }
            _WeaponSet.InsertOne(WeaponSet);
            return Ok(WeaponSet);
        }

        [HttpGet(Name = "Get All Weapon Sets")]
        public IActionResult Get()
        {
            var WeaponSet = _WeaponSet.Find(sheet => true).ToList();
            return Ok(WeaponSet);
        }
    }
}
