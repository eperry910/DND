using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("api/character-class-levels")]
    public class CharacterClassLevelController : ControllerBase
    {
        private readonly IMongoCollection<CharacterClassLevel> _classLevels;
        private readonly ILogger<CharacterClassLevelController> _logger;

        public CharacterClassLevelController(IMongoCollection<CharacterClassLevel> classLevels, ILogger<CharacterClassLevelController> logger)
        {
            _classLevels = classLevels;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CharacterClassLevel classLevel)
        {
            if (classLevel == null)
            {
                return BadRequest("Class level cannot be null");
            }
            await _classLevels.InsertOneAsync(classLevel);
            return CreatedAtAction(nameof(GetById), new { id = classLevel.Id }, classLevel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var classLevels = await _classLevels.Find(level => true).ToListAsync();
            return Ok(classLevels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _classLevels.Find(level => level.Id == id).FirstOrDefaultAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("by-character/{characterId}")]
        public async Task<IActionResult> GetByCharacterId(string characterId)
        {
            var classLevels = await _classLevels.Find(level => level.Id == characterId).ToListAsync();
            return Ok(classLevels);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CharacterClassLevel updatedClassLevel)
        {
            var result = await _classLevels.Find(level => level.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                return NotFound();
            }
            updatedClassLevel.Id = id;
            await _classLevels.ReplaceOneAsync(level => level.Id == id, updatedClassLevel);
            return Ok(updatedClassLevel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _classLevels.DeleteOneAsync(level => level.Id == id);
            if (result.DeletedCount == 0) return NotFound();
            return NoContent();
        }
    }
} 