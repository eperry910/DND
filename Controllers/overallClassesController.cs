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
        [HttpGet("{id}", Name = "Get Class")]
        public IActionResult Get(string id)
        {
            var result = _OverallClasses.Find(sheet => sheet.Name == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPut("{id}", Name = "Update Class")]
        public IActionResult Put(string id, [FromBody] OverallClasses updatedOverallClasses)
        {
            var result = _OverallClasses.Find(sheet => sheet.Name == id).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            updatedOverallClasses.Name = id;
            _OverallClasses.ReplaceOne(sheet => sheet.Name == id, updatedOverallClasses);
            return Ok(updatedOverallClasses);
        }
        [HttpPost("{id}/Subclass", Name = "Add Subclass")]
        public IActionResult AddSubclass(string id, [FromBody] OverallClasses updatedOverallClasses)
         {
            var result = _OverallClasses.Find(sheet => sheet.Name == id).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            result.Subclasses.Add(new Subclass(){
                Name = updatedOverallClasses.Subclasses[0].Name,
                SubclassFeatures = updatedOverallClasses.Subclasses[0].SubclassFeatures
            });
            _OverallClasses.ReplaceOne(sheet => sheet.Name == id, result);
            return Ok(result);
        }
        [HttpPost("AddClass", Name="Add Class")]
        public IActionResult AddClass([FromBody] OverallClasses newClass)
        {
            _OverallClasses.InsertOne(newClass);
            return Ok(newClass);
        }
    }
}
