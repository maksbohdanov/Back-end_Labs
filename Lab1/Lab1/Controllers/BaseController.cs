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

    }
}

