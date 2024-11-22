using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OverallClassesController : ControllerBase
    {
        private readonly IMongoCollection<OverallClasses> _OverallClasses;
        private readonly ILogger<OverallClassesController> _logger;

        public OverallClassesController(IMongoCollection<OverallClasses> AllClasses, ILogger<OverallClassesController> logger)
        {
            _OverallClasses = AllClasses;
            _logger = logger;
        }

        [HttpPost(Name = "New Classes")]
        public IActionResult Post([FromBody] OverallClasses AllClasses)
        {
            if (AllClasses == null)
            {
                return BadRequest("Character sheet cannot be null");
            }
            _OverallClasses.InsertOne(AllClasses);
            return Ok(AllClasses);
        }

        [HttpGet(Name = "Get All Classes")]
        public IActionResult Get()
        {
            var OverallClasses = _OverallClasses.Find(sheet => true).ToList();
            return Ok(OverallClasses);
        }
    }
}
