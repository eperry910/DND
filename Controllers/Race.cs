using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/races")]
	public class RaceController : ControllerBase
	{
		private readonly IMongoCollection<Race> _races;
		private readonly ILogger<RaceController> _logger;

		public RaceController(IMongoCollection<Race> RaceSet, ILogger<RaceController> logger)
		{
			_races = RaceSet;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Race race)
		{
			if (race == null)
			{
				return BadRequest("Race cannot be null");
			}
			await _races.InsertOneAsync(race);
			return CreatedAtAction(nameof(GetByName), new { name = race.Name }, race);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var results = await _races.Find(_ => true).ToListAsync();
			return Ok(results);
		}

		[HttpGet("{name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			var result = await _races.Find(r => r.Name == name).FirstOrDefaultAsync();
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpPut("{name}")]
		public async Task<IActionResult> Update(string name, [FromBody] Race updated)
		{
			var existing = await _races.Find(r => r.Name == name).FirstOrDefaultAsync();
			if (existing == null) return NotFound();
			updated.Name = name;
			await _races.ReplaceOneAsync(r => r.Name == name, updated);
			return Ok(updated);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _races.DeleteOneAsync(r => r.Name == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
}
