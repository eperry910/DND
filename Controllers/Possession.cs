using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/possessions")]
	public class PossessionController : ControllerBase
	{
		private readonly IMongoCollection<Possession> _possessions;
		private readonly ILogger<PossessionController> _logger;

		public PossessionController(IMongoCollection<Possession> PossessionSet, ILogger<PossessionController> logger)
		{
			_possessions = PossessionSet;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Possession possession)
		{
			if (possession == null)
			{
				return BadRequest("Possession cannot be null");
			}
			await _possessions.InsertOneAsync(possession);
			return CreatedAtAction(nameof(GetByName), new { name = possession.Name }, possession);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var possessions = await _possessions.Find(_ => true).ToListAsync();
			return Ok(possessions);
		}

		[HttpGet("{name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			var result = await _possessions.Find(p => p.Name == name).FirstOrDefaultAsync();
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpPut("{name}")]
		public async Task<IActionResult> Update(string name, [FromBody] Possession updated)
		{
			var existing = await _possessions.Find(p => p.Name == name).FirstOrDefaultAsync();
			if (existing == null) return NotFound();
			updated.Name = name;
			await _possessions.ReplaceOneAsync(p => p.Name == name, updated);
			return Ok(updated);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _possessions.DeleteOneAsync(p => p.Name == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
}
