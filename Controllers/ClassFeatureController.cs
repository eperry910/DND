using Microsoft.AspNetCore.Mvc;
using DND.Models;
using MongoDB.Driver;

namespace DND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<ActionResult<List<ClassFeature>>> Get()
        {
            var classFeatures = await _classFeatures.Find(cf => true).ToListAsync();
            return Ok(classFeatures);
        }

        [HttpPost ("AddSingleFeature", Name="Adds A Single Feature")]
        public async Task<ActionResult<ClassFeature>> Post(ClassFeature classFeature)
        {
            var existingFeature = await _classFeatures.Find(cf => cf.FeatureName == classFeature.FeatureName).FirstOrDefaultAsync();
            if (existingFeature != null)
            {
                return BadRequest("Feature with the same name already exists.");
            }
            await _classFeatures.InsertOneAsync(classFeature);
            return CreatedAtAction(nameof(Get), new { id = classFeature.FeatureName }, classFeature);
        }

        [HttpPost("AddingMultipleFeatures", Name = "Add Multiple Features")]
        public async Task<IActionResult> Post([FromBody] List<ClassFeature> Features)
        {
            if (Features == null)
            {
                return BadRequest("Subclass Set cannot be null");
            }

            var featureNames = Features.Select(f => f.FeatureName).ToList();
            var existingFeatures = await _classFeatures.Find(cf => featureNames.Contains(cf.FeatureName)).ToListAsync();

            var newFeatures = Features.Where(f => !existingFeatures.Any(ef => ef.FeatureName == f.FeatureName)).ToList();

            if (newFeatures.Count == 0)
            {
                return BadRequest("All features already exist.");
            }

            await _classFeatures.InsertManyAsync(newFeatures);
            return Ok(newFeatures);
        }
    }
}