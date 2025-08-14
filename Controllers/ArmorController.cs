using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/armors")]
	public class ArmorController : ControllerBase
	{
		private readonly IMongoCollection<Armor> _armorSet;
		private readonly ILogger<ArmorController> _logger;

		public ArmorController(IMongoCollection<Armor> armorSet, ILogger<ArmorController> logger)
		{
			_armorSet = armorSet;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Armor armor)
		{
			if (armor == null)
			{
				return BadRequest("Armor cannot be null");
			}
			await _armorSet.InsertOneAsync(armor);
			return CreatedAtAction(nameof(GetByName), new { name = armor.Name }, armor);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _armorSet.Find(_ => true).ToListAsync();
			return Ok(result);
		}

		[HttpGet("{name}")]
		public async Task<IActionResult> GetByName(string name){
			var result = await _armorSet.Find(sheet => sheet.Name == name).FirstOrDefaultAsync();
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpPut("{name}")]
		public async Task<IActionResult> Update(string name, [FromBody] Armor updatedArmor)
		{
			var existing = await _armorSet.Find(a => a.Name == name).FirstOrDefaultAsync();
			if (existing == null) return NotFound();
			updatedArmor.Name = name;
			await _armorSet.ReplaceOneAsync(a => a.Name == name, updatedArmor);
			return Ok(updatedArmor);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _armorSet.DeleteOneAsync(a => a.Name == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
}
