using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/spells")]
	public class SpellController : ControllerBase
	{
		private readonly IMongoCollection<Spell> _spells;
		private readonly ILogger<SpellController> _logger;

		public SpellController(IMongoCollection<Spell> SpellSet, ILogger<SpellController> logger)
		{
			_spells = SpellSet;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Spell spell)
		{
			if (spell == null)
			{
				return BadRequest("Spell cannot be null");
			}
			await _spells.InsertOneAsync(spell);
			return CreatedAtAction(nameof(GetByName), new { name = spell.Name }, spell);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var spells = await _spells.Find(_ => true).ToListAsync();
			return Ok(spells);
		}

		[HttpGet("{name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			var result = await _spells.Find(spell => spell.Name == name).FirstOrDefaultAsync();
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpPut("{name}")]
		public async Task<IActionResult> Update(string name, [FromBody] Spell updatedSpell)
		{
			var spell = await _spells.Find<Spell>(s => s.Name == name).FirstOrDefaultAsync();
			if (spell == null)
			{
				return NotFound();
			}
			updatedSpell.Name = name;
			await _spells.ReplaceOneAsync(s => s.Name == name, updatedSpell);
			return Ok(updatedSpell);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _spells.DeleteOneAsync(s => s.Name == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
}
