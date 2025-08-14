using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/weapons")]
	public class WeaponController : ControllerBase
	{
		private readonly IMongoCollection<Weapon> _weapons;
		private readonly ILogger<WeaponController> _logger;

		public WeaponController(IMongoCollection<Weapon> WeaponSet, ILogger<WeaponController> logger)
		{
			_weapons = WeaponSet;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Weapon weapon)
		{
			if (weapon == null)
			{
				return BadRequest("Weapon cannot be null");
			}
			await _weapons.InsertOneAsync(weapon);
			return CreatedAtAction(nameof(GetByName), new { name = weapon.Name }, weapon);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var weapons = await _weapons.Find(_ => true).ToListAsync();
			return Ok(weapons);
		}

		[HttpGet("{name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			var weapon = await _weapons.Find(w => w.Name == name).FirstOrDefaultAsync();
			if (weapon == null) return NotFound();
			return Ok(weapon);
		}

		[HttpPut("{name}")]
		public async Task<IActionResult> Update(string name, [FromBody] Weapon updated)
		{
			var existing = await _weapons.Find(w => w.Name == name).FirstOrDefaultAsync();
			if (existing == null) return NotFound();
			updated.Name = name;
			await _weapons.ReplaceOneAsync(w => w.Name == name, updated);
			return Ok(updated);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _weapons.DeleteOneAsync(w => w.Name == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
}
