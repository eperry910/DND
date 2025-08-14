using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("api/character-resources")]
    public class CharacterResourceController : ControllerBase
    {
        private readonly IMongoCollection<CharacterResource> _resources;
        private readonly ILogger<CharacterResourceController> _logger;

        public CharacterResourceController(IMongoCollection<CharacterResource> resources, ILogger<CharacterResourceController> logger)
        {
            _resources = resources;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CharacterResource resource)
        {
            if (resource == null)
            {
                return BadRequest("Resource cannot be null");
            }
            await _resources.InsertOneAsync(resource);
            return CreatedAtAction(nameof(GetById), new { id = resource.Id }, resource);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resources = await _resources.Find(resource => true).ToListAsync();
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _resources.Find(resource => resource.Id == id).FirstOrDefaultAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("by-character/{characterId}")]
        public async Task<IActionResult> GetByCharacterId(string characterId)
        {
            var resources = await _resources.Find(resource => resource.Id == characterId).ToListAsync();
            return Ok(resources);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CharacterResource updatedResource)
        {
            var result = await _resources.Find(resource => resource.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                return NotFound();
            }
            updatedResource.Id = id;
            await _resources.ReplaceOneAsync(resource => resource.Id == id, updatedResource);
            return Ok(updatedResource);
        }

        [HttpPut("{id}/use")]
        public async Task<IActionResult> UseResource(string id, [FromBody] int amount)
        {
            var resource = await _resources.Find(r => r.Id == id).FirstOrDefaultAsync();
            if (resource == null) return NotFound();
            
            if (resource.Current < amount)
            {
                return BadRequest($"Not enough {resource.Name} available. Current: {resource.Current}, Requested: {amount}");
            }
            
            resource.Current -= amount;
            await _resources.ReplaceOneAsync(r => r.Id == id, resource);
            return Ok(resource);
        }

        [HttpPut("{id}/restore")]
        public async Task<IActionResult> RestoreResource(string id, [FromBody] int amount)
        {
            var resource = await _resources.Find(r => r.Id == id).FirstOrDefaultAsync();
            if (resource == null) return NotFound();
            
            resource.Current = Math.Min(resource.Current + amount, resource.Maximum);
            await _resources.ReplaceOneAsync(r => r.Id == id, resource);
            return Ok(resource);
        }

        [HttpPut("{id}/reset")]
        public async Task<IActionResult> ResetResource(string id)
        {
            var resource = await _resources.Find(r => r.Id == id).FirstOrDefaultAsync();
            if (resource == null) return NotFound();
            
            resource.Current = resource.Maximum;
            await _resources.ReplaceOneAsync(r => r.Id == id, resource);
            return Ok(resource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _resources.DeleteOneAsync(resource => resource.Id == id);
            if (result.DeletedCount == 0) return NotFound();
            return NoContent();
        }
    }
} 