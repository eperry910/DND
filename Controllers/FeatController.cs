using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/feats")]
	public class FeatController : ControllerBase
	{
		private readonly IMongoCollection<Feats> _feats;
		private readonly ILogger<FeatController> _logger;

		public FeatController(IMongoCollection<Feats> feats, ILogger<FeatController> logger)
		{
			_feats = feats;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Feats feat)
		{
			if (feat == null) return BadRequest("Feat cannot be null");
			await _feats.InsertOneAsync(feat);
			return CreatedAtAction(nameof(GetByName), new { name = feat.Name }, feat);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _feats.Find(_ => true).ToListAsync();
			return Ok(result);
		}

		[HttpGet("{name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			var result = await _feats.Find(f => f.Name == name).FirstOrDefaultAsync();
			if (result == null) return NotFound();
			return Ok(result);
		}

		[HttpPut("{name}")]
		public async Task<IActionResult> Update(string name, [FromBody] Feats updatedFeat)
		{
			var existing = await _feats.Find(f => f.Name == name).FirstOrDefaultAsync();
			if (existing == null) return NotFound();
			updatedFeat.Name = name;
			await _feats.ReplaceOneAsync(f => f.Name == name, updatedFeat);
			return Ok(updatedFeat);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _feats.DeleteOneAsync(f => f.Name == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
}
