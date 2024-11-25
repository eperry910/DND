using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpellController : ControllerBase
    {
        private readonly IMongoCollection<Spell> _SpellSet;
        private readonly ILogger<SpellController> _logger;

        public SpellController(IMongoCollection<Spell> SpellSet, ILogger<SpellController> logger)
        {
            _SpellSet = SpellSet;
            _logger = logger;
        }

        [HttpPost(Name = "New Spell Set")]
        public IActionResult Post([FromBody] Spell SpellSet)
        {
            if (SpellSet == null)
            {
                return BadRequest("Spell Set cannot be null");
            }
            _SpellSet.InsertOne(SpellSet);
            return Ok(SpellSet);
        }

        [HttpGet(Name = "Get All Spell Sets")]
        public IActionResult Get()
        {
            var SpellSet = _SpellSet.Find(sheet => true).ToList();
            return Ok(SpellSet);
        }
        [HttpPut(Name = "Update Spell")]
        public IActionResult Put(string id, [FromBody] Spell updatedSpell)
        {
            var spell = _SpellSet.Find<Spell>(spell => spell.Name == id).FirstOrDefault();
            if (spell == null)
            {
                return NotFound();
            }
            _SpellSet.ReplaceOne(spell => spell.Name == id, updatedSpell);
            return Ok(updatedSpell);
        }
    }
}
