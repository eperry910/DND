using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using DND.Models;
namespace DND.Controllers;

[ApiController]
[Route("[controller]")]
public class CharacterSheetController : ControllerBase
{
    private static List<CharacterSheet> characterSheets = new List<CharacterSheet>();

    private readonly ILogger<CharacterSheetController> _logger;

    public CharacterSheetController(ILogger<CharacterSheetController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "New Character")]
    public IActionResult Post([FromBody] CharacterSheet characterSheet)
    {
        if(characterSheet == null){
            return BadRequest("Character sheet cannot be null");
        }
        characterSheets.Add(characterSheet);
        return Ok(characterSheet);
        
    }
    [HttpGet(Name = "Classes")]
    public IActionResult Get()
    {
        return Ok(characterSheets);
    }
}
