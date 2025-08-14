using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("api/equipped-items")]
    public class EquippedItemController : ControllerBase
    {
        private readonly IMongoCollection<EquippedItem> _equippedItems;
        private readonly ILogger<EquippedItemController> _logger;

        public EquippedItemController(IMongoCollection<EquippedItem> equippedItems, ILogger<EquippedItemController> logger)
        {
            _equippedItems = equippedItems;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EquippedItem equippedItem)
        {
            if (equippedItem == null)
            {
                return BadRequest("Equipped item cannot be null");
            }
            await _equippedItems.InsertOneAsync(equippedItem);
            return CreatedAtAction(nameof(GetById), new { id = equippedItem.Id }, equippedItem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var equippedItems = await _equippedItems.Find(item => true).ToListAsync();
            return Ok(equippedItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _equippedItems.Find(item => item.Id == id).FirstOrDefaultAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("by-character/{characterId}")]
        public async Task<IActionResult> GetByCharacterId(string characterId)
        {
            var equippedItems = await _equippedItems.Find(item => item.Id == characterId).ToListAsync();
            return Ok(equippedItems);
        }

        [HttpGet("by-slot/{slot}")]
        public async Task<IActionResult> GetBySlot(string slot)
        {
            var equippedItems = await _equippedItems.Find(item => item.Slot == slot).ToListAsync();
            return Ok(equippedItems);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] EquippedItem updatedEquippedItem)
        {
            var result = await _equippedItems.Find(item => item.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                return NotFound();
            }
            updatedEquippedItem.Id = id;
            await _equippedItems.ReplaceOneAsync(item => item.Id == id, updatedEquippedItem);
            return Ok(updatedEquippedItem);
        }

        [HttpPut("{id}/toggle-attunement")]
        public async Task<IActionResult> ToggleAttunement(string id)
        {
            var equippedItem = await _equippedItems.Find(item => item.Id == id).FirstOrDefaultAsync();
            if (equippedItem == null) return NotFound();
            
            equippedItem.IsAttuned = !equippedItem.IsAttuned;
            await _equippedItems.ReplaceOneAsync(item => item.Id == id, equippedItem);
            return Ok(equippedItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _equippedItems.DeleteOneAsync(item => item.Id == id);
            if (result.DeletedCount == 0) return NotFound();
            return NoContent();
        }
    }
} 