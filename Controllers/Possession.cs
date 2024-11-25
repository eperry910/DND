using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PossessionController : ControllerBase
    {
        private readonly IMongoCollection<Possession> _PossessionSet;
        private readonly ILogger<PossessionController> _logger;

        public PossessionController(IMongoCollection<Possession> PossessionSet, ILogger<PossessionController> logger)
        {
            _PossessionSet = PossessionSet;
            _logger = logger;
        }

        [HttpPost(Name = "New Possession Set")]
        public IActionResult Post([FromBody] Possession PossessionSet)
        {
            if (PossessionSet == null)
            {
                return BadRequest("Possession Set cannot be null");
            }
            _PossessionSet.InsertOne(PossessionSet);
            return Ok(PossessionSet);
        }

        [HttpGet(Name = "Get All Possession Sets")]
        public IActionResult Get()
        {
            var PossessionSet = _PossessionSet.Find(sheet => true).ToList();
            return Ok(PossessionSet);
        }
    }
}
