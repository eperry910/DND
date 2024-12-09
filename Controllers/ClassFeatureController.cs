
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

        [HttpPost]
        public async Task<ActionResult<ClassFeature>> Post(ClassFeature classFeature)
        {
            await _classFeatures.InsertOneAsync(classFeature);
            return CreatedAtAction(nameof(Get), new { id = classFeature.FeatureName }, classFeature);
        }
        [HttpPost("AddingFeatures", Name = "Add Multiple Features")]
        public IActionResult Post([FromBody] List<ClassFeature> Features)
        {
            if (Features == null)
            {
                return BadRequest("Subclass Set cannot be null");
            }
            _classFeatures.InsertMany(Features);
            return Ok(Features);
        }
    }
}