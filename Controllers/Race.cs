using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RaceController : ControllerBase
    {
        private readonly IMongoCollection<Race> _RaceSet;
        private readonly ILogger<RaceController> _logger;

        public RaceController(IMongoCollection<Race> RaceSet, ILogger<RaceController> logger)
        {
            _RaceSet = RaceSet;
            _logger = logger;
        }

        [HttpPost(Name = "New Race Set")]
        public IActionResult Post([FromBody] Race RaceSet)
        {
            if (RaceSet == null)
            {
                return BadRequest("Race Set cannot be null");
            }
            _RaceSet.InsertOne(RaceSet);
            return Ok(RaceSet);
        }

        [HttpGet(Name = "Get All Race Sets")]
        public IActionResult Get()
        {
            var RaceSet = _RaceSet.Find(sheet => true).ToList();
            return Ok(RaceSet);
        }
    }
}
