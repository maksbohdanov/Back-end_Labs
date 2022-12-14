using AutoMapper;
using CostAccounting.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.Entities.DTOs;
using WebApi.Models;

namespace CostAccounting.Controllers
{
    [ApiController]
    [Route("api")]
    public class CostsController : ControllerBase
    {
        private readonly CostAccountingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public CostsController(CostAccountingDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _mapper= mapper;
            _context = context;
            _configuration = configuration;
            _context.Database.EnsureCreated();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginModel model)
        {
            if (!_context.Users.Any(x => x.Email == model.Email))
                return NotFound("User with such email not found");

            var user = _context.Users.First(x => 
                x.Email == model.Email);

            if (user.Password != model.Password)
                return BadRequest("Wrong password.");

            var token = TokenGenerator.GenerateToken(user, _configuration);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult Register([FromBody] RegistrationModel model)
        {
            if (_context.Users.Any(x => x.Email == model.Email))
                return BadRequest("User with such email already exists.");

            var user = new User()
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,

            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Register), user);
        }

        [HttpGet("categories")]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories([FromQuery] int? userId)
        {
            
            var customFlag = userId != null;
            var categories = _context.Categories.Where(
                c => userId == c.UserId && c.IsCustom == customFlag);
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

            category.UserId = userId;
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

