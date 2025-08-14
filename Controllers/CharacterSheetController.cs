using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/character-sheets")]
	public class CharacterSheetController : ControllerBase
	{
		private readonly IMongoCollection<CharacterSheet> _characterSheets;
		private readonly ILogger<CharacterSheetController> _logger;

		public CharacterSheetController(IMongoCollection<CharacterSheet> characterSheets, ILogger<CharacterSheetController> logger)
		{
			_characterSheets = characterSheets;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CharacterSheet characterSheet)
		{
			if (characterSheet == null)
			{
				return BadRequest("Character sheet cannot be null");
			}
			await _characterSheets.InsertOneAsync(characterSheet);
			return CreatedAtAction(nameof(GetById), new { id = characterSheet.Id }, characterSheet);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var characterSheets = await _characterSheets.Find(sheet => true).ToListAsync();
			return Ok(characterSheets);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			if (!ObjectId.TryParse(id, out ObjectId objectId))
			{
				return BadRequest("Invalid ObjectId format");
			}
			var result = await _characterSheets.Find(sheet => sheet.Id == id).FirstOrDefaultAsync();
			if (result == null) return NotFound();
			return Ok(result);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(string id, [FromBody] CharacterSheet updatedCharacterSheet)
		{
			if (!ObjectId.TryParse(id, out ObjectId objectId))
			{
				return BadRequest("Invalid ObjectId format");
			}
			var result = await _characterSheets.Find(sheet => sheet.Id == id).FirstOrDefaultAsync();
			if (result == null)
			{
				return NotFound();
			}
			updatedCharacterSheet.Id = id;
			await _characterSheets.ReplaceOneAsync(sheet => sheet.Id == id, updatedCharacterSheet);
			return Ok(updatedCharacterSheet);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			if (!ObjectId.TryParse(id, out ObjectId objectId))
			{
				return BadRequest("Invalid ObjectId format");
			}
			var result = await _characterSheets.DeleteOneAsync(s => s.Id == id);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
}
