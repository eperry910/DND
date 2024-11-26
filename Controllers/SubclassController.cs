using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubclassController : ControllerBase
    {
        private readonly IMongoCollection<Subclass> _SubclassSet;
        private readonly ILogger<SubclassController> _logger;

        public SubclassController(IMongoCollection<Subclass> SubclassSet, ILogger<SubclassController> logger)
        {
            _SubclassSet = SubclassSet;
            _logger = logger;
        }

        [HttpPost(Name = "New Subclass")]
        public IActionResult Post([FromBody] Subclass SubclassSet)
        {
            if (SubclassSet == null)
            {
                return BadRequest("Subclass Set cannot be null");
            }
            _SubclassSet.InsertOne(SubclassSet);
            return Ok(SubclassSet);
        }

        [HttpGet(Name = "Get All Subclass Sets")]
        public IActionResult Get()
        {
            var SubclassSet = _SubclassSet.Find(sheet => true).ToList();
            return Ok(SubclassSet);
        }
        [HttpPost("AddingSubclasses", Name = "Add Multiple Subclasses")]
        public IActionResult Post([FromBody] List<Subclass> SubclassSet)
        {
            if (SubclassSet == null)
            {
                return BadRequest("Subclass Set cannot be null");
            }
            _SubclassSet.InsertMany(SubclassSet);
            return Ok(SubclassSet);
        }
    }
}
