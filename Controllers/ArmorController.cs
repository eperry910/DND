using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArmorController : ControllerBase
    {
        private readonly IMongoCollection<Armor> _armorSet;
        private readonly ILogger<ArmorController> _logger;

        public ArmorController(IMongoCollection<Armor> armorSet, ILogger<ArmorController> logger)
        {
            _armorSet = armorSet;
            _logger = logger;
        }

        [HttpPost(Name = "New Armor Set")]
        public IActionResult Post([FromBody] Armor armorSet)
        {
            if (armorSet == null)
            {
                return BadRequest("Armor Set cannot be null");
            }
            _armorSet.InsertOne(armorSet);
            return Ok(armorSet);
        }

        [HttpGet(Name = "Get All Armor Sets")]
        public IActionResult Get()
        {
            var armorSet = _armorSet.Find(sheet => true).ToList();
            return Ok(armorSet);
        }
        [HttpGet("{name}",Name = "Get Specified Armor Set")]
        public IActionResult Get(string name){
            var armorSet = _armorSet.Find(sheet => sheet.Name == name);
            return Ok(armorSet);
        }
    }
}
