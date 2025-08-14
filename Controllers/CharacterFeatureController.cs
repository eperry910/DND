using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("api/character-features")]
    public class CharacterFeatureController : ControllerBase
    {
        private readonly IMongoCollection<CharacterFeature> _features;
        private readonly ILogger<CharacterFeatureController> _logger;

        public CharacterFeatureController(IMongoCollection<CharacterFeature> features, ILogger<CharacterFeatureController> logger)
        {
            _features = features;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CharacterFeature feature)
        {
            if (feature == null)
            {
                return BadRequest("Feature cannot be null");
            }
            await _features.InsertOneAsync(feature);
            return CreatedAtAction(nameof(GetById), new { id = feature.Id }, feature);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var features = await _features.Find(feature => true).ToListAsync();
            return Ok(features);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _features.Find(feature => feature.Id == id).FirstOrDefaultAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("by-character/{characterId}")]
        public async Task<IActionResult> GetByCharacterId(string characterId)
        {
            var features = await _features.Find(feature => feature.Id == characterId).ToListAsync();
            return Ok(features);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveFeatures()
        {
            var features = await _features.Find(feature => feature.IsActive).ToListAsync();
            return Ok(features);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CharacterFeature updatedFeature)
        {
            var result = await _features.Find(feature => feature.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                return NotFound();
            }
            updatedFeature.Id = id;
            await _features.ReplaceOneAsync(feature => feature.Id == id, updatedFeature);
            return Ok(updatedFeature);
        }

        [HttpPut("{id}/toggle")]
        public async Task<IActionResult> ToggleActive(string id)
        {
            var feature = await _features.Find(f => f.Id == id).FirstOrDefaultAsync();
            if (feature == null) return NotFound();
            
            feature.IsActive = !feature.IsActive;
            await _features.ReplaceOneAsync(f => f.Id == id, feature);
            return Ok(feature);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _features.DeleteOneAsync(feature => feature.Id == id);
            if (result.DeletedCount == 0) return NotFound();
            return NoContent();
        }
    }
} 