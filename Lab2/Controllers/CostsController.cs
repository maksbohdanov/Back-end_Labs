using AutoMapper;
using CostAccounting.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities.DTOs;

namespace CostAccounting.Controllers
{
    [ApiController]
    public class CostsController : ControllerBase
    {
        private readonly CostAccountingDbContext _context;
        private readonly IMapper _mapper;

        public CostsController(CostAccountingDbContext context, IMapper mapper)
        {
            _mapper= mapper;
            _context = context;
            _context.Database.EnsureCreated();
        }

        [HttpGet("categories")]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories([FromQuery] int? userId)
        {
            
            var customFlag = userId != null;
            var categories = _context.Categories.Where(
                c => userId == c.UserId && c.IsCustom == customFlag);
           // var categories = _context.Categories.Where(x => !(x.IsCustom));
            if (categories == null)
                return NotFound();

            var result = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return Ok(result);
        }

        [HttpGet("records/user/{userId}")]
        public ActionResult<IEnumerable<RecordDto>> GetRecordsByUserId(int userId)
        {           
            var records = _context.Records.Include(x => x.User).Include(x => x.Category)
                .Where(r => r.UserId == userId &&
                    (!r.Category.IsCustom || r.Category.UserId == userId)) 
                    .ToList();
            if (records.Count == 0)
                return NotFound();

            var result = _mapper.Map<IEnumerable<RecordDto>>(records);
            return Ok(result);
        }

        [HttpGet("records/user/{userId}/category/{categoryId}")]
        public ActionResult<IEnumerable<RecordDto>> GetRecordsByCategoryAndUserId(int userId, int categoryId)
        {
            var records = _context.Records
                .Where(r => r.UserId == userId &&
                    (r.User.Categories.Any(c => c.UserId == userId) || 
                    r.Category.IsCustom == false) && 
                    r.CategoryId == categoryId).ToList();
            if (records.Count == 0)
                return NotFound();

            var result = _mapper.Map<IEnumerable<RecordDto>>(records);

            return Ok(result);
        }

        [HttpPost("users/add")]
        public ActionResult AddUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Cannot add new user");

            if (_context.Users.Any(u => u.Id == user.Id))
                return BadRequest("User with such id already exists");

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User has been created");
        }       

        [HttpPost("categories/add")]
        public ActionResult AddCategory([FromBody] Category category)
        {
            if (category == null)
                return BadRequest("Cannot add new category");

            if (_context.Categories.Any(u => u.Id == category.Id))
                return BadRequest("Category with such id already exists");            

            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok("Category has been created");
        }

        [HttpPost("users/{userId}/categories/add")]
        public ActionResult AddCustomCategory([FromBody] Category category, int userId)
        {
            if (category == null)
                return BadRequest("Cannot add new category");

            if (_context.Categories.Any(u => u.Id == category.Id))
                return BadRequest("Category with such id already exists");

            category.Id = userId;
            category.IsCustom= true;

            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok("Category has been created");
        }

        [HttpPost("records/add")]
        public ActionResult AddRecord([FromBody] Record record)
        {
            if (record == null)
                return BadRequest("Cannot create a record");

            if (_context.Records.Any(u => u.Id == record.Id))
                return BadRequest("Record with such id already exists");

            if (!_context.Users.Any(u => u.Id == record.UserId))
                return NotFound("User with such id not found");

            if (!_context.Categories.Any(c => c.Id == record.CategoryId))
                return NotFound("Category with such id not found");

            _context.Records.Add(record);
            _context.SaveChanges();
            return Ok("Record has been added");
        }
    }
}

