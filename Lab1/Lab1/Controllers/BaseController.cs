using Lab1.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Lab1Controller : ControllerBase
    {
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = DbContext.Categories;
            if(categories == null)
                return NotFound();

            return Ok(categories);
        }

        [HttpGet("records/user/{id}")]
        public async Task<ActionResult<IEnumerable<Record>>> GetRecordsByUserId(int id)
        {
            var records = DbContext.Records.Where(r => r.UserId == id).ToList();
            if (records.Count == 0)
                return NotFound();

            return Ok(records);
        }

        [HttpGet("records/category/")]
        public async Task<ActionResult<IEnumerable<Record>>> GetRecordsByCategoryAndUserId([FromQuery]int userId, [FromQuery]int categoryId)
        {
            var records = DbContext.Records.Where(
                r => r.UserId == userId && r.CategoryId == categoryId).ToList();
            if (records.Count == 0)
                return NotFound();
                
            return Ok(records);
        }

        [HttpPost("users/add")]
        public async Task<ActionResult> AddUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Cannot add new user");

            if(DbContext.Users.Any(u => u.Id == user.Id))
                return BadRequest("User with such id already exists");

            DbContext.Users.Add(user);
            return Ok("User has been created");
        }

        [HttpPost("categories/add")]
        public async Task<ActionResult> AddCategory([FromBody] Category category)
        {
            if (category == null)
                return BadRequest("Cannot add new category");

            if (DbContext.Categories.Any(u => u.Id == category.Id))
                return BadRequest("Category with such id already exists");

            DbContext.Categories.Add(category);
            return Ok("Category has been created");
        }

        [HttpPost("records/add")]
        public async Task<ActionResult> AddRecord([FromBody] Record record)
        {
            if (record == null)
                return BadRequest("Cannot create a record");

            if (DbContext.Records.Any(u => u.Id == record.Id))
                return BadRequest("Record with such id already exists");

            if(!DbContext.Users.Any(u => u.Id == record.UserId))
                return NotFound("User with such id not found");
            
            if(!DbContext.Categories.Any(c => c.Id == record.CategoryId))
                return NotFound("Category with such id not found");

            DbContext.Records.Add(record);
            return Ok("Record has been added");
        }

    }
}

