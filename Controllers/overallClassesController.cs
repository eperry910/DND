using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/classes")]
	public class OverallClassesController : ControllerBase
	{
		private readonly IMongoCollection<OverallClasses> _OverallClasses;
		private readonly ILogger<OverallClassesController> _logger;

		public OverallClassesController(IMongoCollection<OverallClasses> AllClasses, ILogger<OverallClassesController> logger)
		{
			_OverallClasses = AllClasses;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] OverallClasses AllClasses)
		{
			if (AllClasses == null)
			{
				return BadRequest("Class cannot be null");
			}
			await _OverallClasses.InsertOneAsync(AllClasses);
			return CreatedAtAction(nameof(GetByName), new { name = AllClasses.Name }, AllClasses);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var OverallClasses = await _OverallClasses.Find(_ => true).ToListAsync();
			return Ok(OverallClasses);
		}
		[HttpGet("{name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			var result = await _OverallClasses.Find(sheet => sheet.Name == name).FirstOrDefaultAsync();
			if (result == null) return NotFound();
			return Ok(result);
		}
		[HttpPut("{name}")]
		public async Task<IActionResult> Update(string name, [FromBody] OverallClasses updatedOverallClasses)
		{
			var result = await _OverallClasses.Find(sheet => sheet.Name == name).FirstOrDefaultAsync();
			if (result == null)
			{
				return NotFound();
			}
			updatedOverallClasses.Name = name;
			await _OverallClasses.ReplaceOneAsync(sheet => sheet.Name == name, updatedOverallClasses);
			return Ok(updatedOverallClasses);
		}
		[HttpPost("AddClass")]
		public async Task<IActionResult> AddClass([FromBody] OverallClasses newClass)
		{
			await _OverallClasses.InsertOneAsync(newClass);
			return Ok(newClass);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _OverallClasses.DeleteOneAsync(c => c.Name == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
}
