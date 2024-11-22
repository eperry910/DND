// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using MongoDB.Driver;
// using DND.Models;

// namespace DND.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class CharacterClassController : ControllerBase
//     {
//         private readonly IMongoCollection<CharacterClass> _CharacterClass;
//         private readonly ILogger<CharacterClassController> _logger;

//         public CharacterClassController(IMongoCollection<CharacterClass> characterClass, ILogger<CharacterClassController> logger)
//         {
//             _CharacterClass = characterClass;
//             _logger = logger;
//         }

//         [HttpPost(Name = "New Class")]
//         public IActionResult Post([FromBody] CharacterClass characterClass)
//         {
//             if (characterClass == null)
//             {
//                 return BadRequest("Class cannot be null");
//             }
//             _CharacterClass.InsertOne(characterClass);
//             return Ok(characterClass);
//         }

//         [HttpGet(Name = "Get All Classes")]
//         public IActionResult Get()
//         {
//             var characterClass = _CharacterClass.Find(sheet => true).ToList();
//             return Ok(characterClass);
//         }
//         // [HttpPut(Name = "Add New Subclass")]
//         // public IActionResult Put([FromBody] Subclass subclass)
//         // {
//         //     if (subclass == null)
//         //     {
//         //         return BadRequest("Subclass cannot be null");
//         //     }
//         //     _CharacterClass.InsertOne(subclass);
//         //     return Ok(subclass);
//         // }
//     }
// }
