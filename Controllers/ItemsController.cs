using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using DND.Models;

namespace DND.Controllers
{
	[ApiController]
	[Route("api/items")]
	public class ItemsController : ControllerBase
	{
		private readonly IMongoCollection<Item> _items;
		private readonly ILogger<ItemsController> _logger;

		public ItemsController(IMongoCollection<Item> items, ILogger<ItemsController> logger)
		{
			_items = items;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<List<Item>>> GetAll()
		{
			var results = await _items.Find(_ => true).ToListAsync();
			return Ok(results);
		}

		[HttpGet("{name}")]
		public async Task<ActionResult<Item>> GetByName(string name)
		{
			var item = await _items.Find(i => i.Name == name).FirstOrDefaultAsync();
			if (item == null) return NotFound();
			return Ok(item);
		}

		[HttpPost]
		public async Task<ActionResult<Item>> Create([FromBody] Item item)
		{
			if (item == null) return BadRequest();
			await _items.InsertOneAsync(item);
			return CreatedAtAction(nameof(GetByName), new { name = item.Name }, item);
		}

		[HttpPut("{name}")]
		public async Task<ActionResult<Item>> Update(string name, [FromBody] Item updated)
		{
			var existing = await _items.Find(i => i.Name == name).FirstOrDefaultAsync();
			if (existing == null) return NotFound();
			updated.Name = name;
			await _items.ReplaceOneAsync(i => i.Name == name, updated);
			return Ok(updated);
		}

		[HttpDelete("{name}")]
		public async Task<IActionResult> Delete(string name)
		{
			var result = await _items.DeleteOneAsync(i => i.Name == name);
			if (result.DeletedCount == 0) return NotFound();
			return NoContent();
		}
	}
} 