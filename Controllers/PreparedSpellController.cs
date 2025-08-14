using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("api/prepared-spells")]
    public class PreparedSpellController : ControllerBase
    {
        private readonly IMongoCollection<PreparedSpell> _preparedSpells;
        private readonly ILogger<PreparedSpellController> _logger;

        public PreparedSpellController(IMongoCollection<PreparedSpell> preparedSpells, ILogger<PreparedSpellController> logger)
        {
            _preparedSpells = preparedSpells;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PreparedSpell preparedSpell)
        {
            if (preparedSpell == null)
            {
                return BadRequest("Prepared spell cannot be null");
            }
            await _preparedSpells.InsertOneAsync(preparedSpell);
            return CreatedAtAction(nameof(GetById), new { id = preparedSpell.Id }, preparedSpell);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var preparedSpells = await _preparedSpells.Find(spell => true).ToListAsync();
            return Ok(preparedSpells);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _preparedSpells.Find(spell => spell.Id == id).FirstOrDefaultAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("by-character/{characterId}")]
        public async Task<IActionResult> GetByCharacterId(string characterId)
        {
            var preparedSpells = await _preparedSpells.Find(spell => spell.Id == characterId).ToListAsync();
            return Ok(preparedSpells);
        }

        [HttpGet("by-source/{source}")]
        public async Task<IActionResult> GetBySource(string source)
        {
            var preparedSpells = await _preparedSpells.Find(spell => spell.Source == source).ToListAsync();
            return Ok(preparedSpells);
        }

        [HttpGet("always-prepared")]
        public async Task<IActionResult> GetAlwaysPrepared()
        {
            var preparedSpells = await _preparedSpells.Find(spell => spell.IsAlwaysPrepared).ToListAsync();
            return Ok(preparedSpells);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PreparedSpell updatedPreparedSpell)
        {
            var result = await _preparedSpells.Find(spell => spell.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                return NotFound();
            }
            updatedPreparedSpell.Id = id;
            await _preparedSpells.ReplaceOneAsync(spell => spell.Id == id, updatedPreparedSpell);
            return Ok(updatedPreparedSpell);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _preparedSpells.DeleteOneAsync(spell => spell.Id == id);
            if (result.DeletedCount == 0) return NotFound();
            return NoContent();
        }
    }
} 