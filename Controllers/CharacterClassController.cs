using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/character-classes")]
	public class CharacterClassController : ControllerBase
	{
		private readonly IMongoCollection<CharacterClass> _classes;
		private readonly ILogger<CharacterClassController> _logger;

		public CharacterClassController(IMongoCollection<CharacterClass> classes, ILogger<CharacterClassController> logger)
		{
			_classes = classes;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<List<CharacterClass>>> GetAll()
		{
			var results = await _classes.Find(_ => true).ToListAsync();
			return Ok(results);
		}

		[HttpGet("{name}")]
		public async Task<ActionResult<CharacterClass>> GetByName(string name)
		{
			var result = await _classes.Find(c => c.ClassName == name).FirstOrDefaultAsync();
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<CharacterClass>> Create([FromBody] CharacterClass clazz)
		{
			if (clazz == null) return BadRequest();
			await _classes.InsertOneAsync(clazz);
			return CreatedAtAction(nameof(GetByName), new { name = clazz.ClassName }, clazz);
		}

		[HttpPut("{name}")]
		public async Task<ActionResult<CharacterClass>> Update(string name, [FromBody] CharacterClass updated)
		{
			var existing = await _classes.Find(c => c.ClassName == name).FirstOrDefaultAsync();
			if (existing == null) return NotFound();
			updated.ClassName = name;
			await _classes.ReplaceOneAsync(c => c.ClassName == name, updated);
			return Ok(updated);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _classes.DeleteOneAsync(c => c.ClassName == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
} 