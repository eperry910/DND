using Microsoft.AspNetCore.Mvc;
using DND.Models;
using MongoDB.Driver;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/class-features")]
	public class ClassFeatureController : ControllerBase
	{
		private readonly IMongoCollection<ClassFeature> _classFeatures;

		private readonly ILogger<ClassFeatureController> _logger;
		public ClassFeatureController(IMongoCollection<ClassFeature> classFeatures, ILogger<ClassFeatureController> logger)
		{
			_classFeatures = classFeatures;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<List<ClassFeature>>> GetAll()
		{
			var classFeatures = await _classFeatures.Find(cf => true).ToListAsync();
			return Ok(classFeatures);
		}

		[HttpGet("{name}")]
		public async Task<ActionResult<ClassFeature>> GetByName(string name)
		{
			var feature = await _classFeatures.Find(cf => cf.FeatureName == name).FirstOrDefaultAsync();
			if (feature == null) return NotFound();
			return Ok(feature);
		}

		[HttpPost]
		public async Task<ActionResult<ClassFeature>> Create([FromBody] ClassFeature classFeature)
		{
			var existingFeature = await _classFeatures.Find(cf => cf.FeatureName == classFeature.FeatureName).FirstOrDefaultAsync();
			if (existingFeature != null)
			{
				return BadRequest("Feature with the same name already exists.");
			}
			await _classFeatures.InsertOneAsync(classFeature);
			return CreatedAtAction(nameof(GetByName), new { name = classFeature.FeatureName }, classFeature);
		}

		[HttpPost("bulk")]
		public async Task<IActionResult> CreateMany([FromBody] List<ClassFeature> features)
		{
			if (features == null)
			{
				return BadRequest("Features cannot be null");
			}

			var featureNames = features.Select(f => f.FeatureName).ToList();
			var existingFeatures = await _classFeatures.Find(cf => featureNames.Contains(cf.FeatureName)).ToListAsync();

			var newFeatures = features.Where(f => !existingFeatures.Any(ef => ef.FeatureName == f.FeatureName)).ToList();

			if (newFeatures.Count == 0)
			{
				return BadRequest("All features already exist.");
			}

			await _classFeatures.InsertManyAsync(newFeatures);
			return Ok(newFeatures);
		}

		[HttpPut("{name}")]
		public async Task<IActionResult> Update(string name, [FromBody] ClassFeature updated)
		{
			var existing = await _classFeatures.Find(cf => cf.FeatureName == name).FirstOrDefaultAsync();
			if (existing == null) return NotFound();
			updated.FeatureName = name;
			await _classFeatures.ReplaceOneAsync(cf => cf.FeatureName == name, updated);
			return Ok(updated);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _classFeatures.DeleteOneAsync(cf => cf.FeatureName == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
}