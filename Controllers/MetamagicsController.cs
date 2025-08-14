using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/metamagics")]
	public class MetamagicsController : ControllerBase
	{
		private readonly IMongoCollection<Metamagics> _metamagics;
		private readonly ILogger<MetamagicsController> _logger;

		public MetamagicsController(IMongoCollection<Metamagics> metamagics, ILogger<MetamagicsController> logger)
		{
			_metamagics = metamagics;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<List<Metamagics>>> GetAll()
		{
			var results = await _metamagics.Find(_ => true).ToListAsync();
			return Ok(results);
		}

		[HttpGet("{name}")]
		public async Task<ActionResult<Metamagics>> GetByName(string name)
		{
			var result = await _metamagics.Find(m => m.Name == name).FirstOrDefaultAsync();
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<Metamagics>> Create([FromBody] Metamagics meta)
		{
			if (meta == null) return BadRequest();
			await _metamagics.InsertOneAsync(meta);
			return CreatedAtAction(nameof(GetByName), new { name = meta.Name }, meta);
		}

		[HttpPut("{name}")]
		public async Task<ActionResult<Metamagics>> Update(string name, [FromBody] Metamagics updated)
		{
			var existing = await _metamagics.Find(m => m.Name == name).FirstOrDefaultAsync();
			if (existing == null) return NotFound();
			updated.Name = name;
			await _metamagics.ReplaceOneAsync(m => m.Name == name, updated);
			return Ok(updated);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _metamagics.DeleteOneAsync(m => m.Name == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
} 