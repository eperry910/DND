using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/subclasses")]
	public class SubclassController : ControllerBase
	{
		private readonly IMongoCollection<Subclass> _SubclassSet;
		private readonly ILogger<SubclassController> _logger;

		public SubclassController(IMongoCollection<Subclass> SubclassSet, ILogger<SubclassController> logger)
		{
			_SubclassSet = SubclassSet;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Subclass subclass)
		{
			if (subclass == null)
			{
				return BadRequest("Subclass cannot be null");
			}
			await _SubclassSet.InsertOneAsync(subclass);
			return CreatedAtAction(nameof(GetByName), new { name = subclass.Name }, subclass);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var subclasses = await _SubclassSet.Find(_ => true).ToListAsync();
			return Ok(subclasses);
		}

		[HttpGet("{name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			var result = await _SubclassSet.Find(s => s.Name == name).FirstOrDefaultAsync();
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpPut("{name}")]
		public async Task<IActionResult> Update(string name, [FromBody] Subclass updated)
		{
			var existing = await _SubclassSet.Find(s => s.Name == name).FirstOrDefaultAsync();
			if (existing == null) return NotFound();
			updated.Name = name;
			await _SubclassSet.ReplaceOneAsync(s => s.Name == name, updated);
			return Ok(updated);
		}

		[HttpPost("bulk")]
		public async Task<IActionResult> CreateMany([FromBody] List<Subclass> subclasses)
		{
			if (subclasses == null) return BadRequest("Subclass Set cannot be null");
			await _SubclassSet.InsertManyAsync(subclasses);
			return Ok(subclasses);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _SubclassSet.DeleteOneAsync(s => s.Name == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
}
