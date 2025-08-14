using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/eldritch-invocations")]
	public class EldritchInvocationsController : ControllerBase
	{
		private readonly IMongoCollection<EldritchInvocations> _invocations;
		private readonly ILogger<EldritchInvocationsController> _logger;

		public EldritchInvocationsController(IMongoCollection<EldritchInvocations> invocations, ILogger<EldritchInvocationsController> logger)
		{
			_invocations = invocations;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<List<EldritchInvocations>>> GetAll()
		{
			var results = await _invocations.Find(_ => true).ToListAsync();
			return Ok(results);
		}

		[HttpGet("{name}")]
		public async Task<ActionResult<EldritchInvocations>> GetByName(string name)
		{
			var result = await _invocations.Find(i => i.Name == name).FirstOrDefaultAsync();
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<EldritchInvocations>> Create([FromBody] EldritchInvocations invocation)
		{
			if (invocation == null) return BadRequest();
			await _invocations.InsertOneAsync(invocation);
			return CreatedAtAction(nameof(GetByName), new { name = invocation.Name }, invocation);
		}

		[HttpPut("{name}")]
		public async Task<ActionResult<EldritchInvocations>> Update(string name, [FromBody] EldritchInvocations updated)
		{
			var existing = await _invocations.Find(i => i.Name == name).FirstOrDefaultAsync();
			if (existing == null) return NotFound();
			updated.Name = name;
			await _invocations.ReplaceOneAsync(i => i.Name == name, updated);
			return Ok(updated);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _invocations.DeleteOneAsync(i => i.Name == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
} 