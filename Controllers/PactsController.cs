using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/pacts")]
	public class PactsController : ControllerBase
	{
		private readonly IMongoCollection<Pacts> _pacts;
		private readonly ILogger<PactsController> _logger;

		public PactsController(IMongoCollection<Pacts> pacts, ILogger<PactsController> logger)
		{
			_pacts = pacts;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<List<Pacts>>> GetAll()
		{
			var results = await _pacts.Find(_ => true).ToListAsync();
			return Ok(results);
		}

		[HttpGet("{name}")]
		public async Task<ActionResult<Pacts>> GetByName(string name)
		{
			var result = await _pacts.Find(p => p.Name == name).FirstOrDefaultAsync();
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<Pacts>> Create([FromBody] Pacts pact)
		{
			if (pact == null) return BadRequest();
			await _pacts.InsertOneAsync(pact);
			return CreatedAtAction(nameof(GetByName), new { name = pact.Name }, pact);
		}

		[HttpPut("{name}")]
		public async Task<ActionResult<Pacts>> Update(string name, [FromBody] Pacts updated)
		{
			var existing = await _pacts.Find(p => p.Name == name).FirstOrDefaultAsync();
			if (existing == null) return NotFound();
			updated.Name = name;
			await _pacts.ReplaceOneAsync(p => p.Name == name, updated);
			return Ok(updated);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _pacts.DeleteOneAsync(p => p.Name == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
} 