using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterSheetController : ControllerBase
    {
        private readonly IMongoCollection<CharacterSheet> _characterSheets;
        private readonly ILogger<CharacterSheetController> _logger;

        public CharacterSheetController(IMongoCollection<CharacterSheet> characterSheets, ILogger<CharacterSheetController> logger)
        {
            _characterSheets = characterSheets;
            _logger = logger;
        }

        [HttpPost(Name = "New Character")]
        public IActionResult Post([FromBody] CharacterSheet characterSheet)
        {
            if (characterSheet == null)
            {
                return BadRequest("Character sheet cannot be null");
            }
            _characterSheets.InsertOne(characterSheet);
            return Ok(characterSheet);
        }

        [HttpGet(Name = "Get All Characters")]
        public IActionResult Get()
        {
            var characterSheets = _characterSheets.Find(sheet => true).ToList();
            return Ok(characterSheets);
        }
        [HttpGet("{id}", Name = "Get Character")]
        public IActionResult Get(string id)
        {
            var result = _characterSheets.Find(sheet => sheet.Id == id).FirstOrDefault();
            return Ok(result);
        }
        [HttpPut("{id}", Name = "Update Character")]
        public IActionResult Put(string id, [FromBody] CharacterSheet updatedCharacterSheet)
        {
            var result = _characterSheets.Find(sheet => sheet.Id == id).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            updatedCharacterSheet.Id = id;
            _characterSheets.ReplaceOne(sheet => sheet.Id == id, updatedCharacterSheet);
            return Ok(updatedCharacterSheet);
        }
    }
}
