using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;
using WebApplication17.DTOs;
using WebApplication17.Filters;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _db;
        public DepartmentController(AppDbContext db) => _db = db;


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll()
        {
            var depts = await _db.Departments.AsNoTracking().ToListAsync();
            var dto = depts.Select(d => new DepartmentDto
            {
                Name = d.Name,
                Manager = d.Manager,
                Location = d.Location,
                NumOfStd = d.NumOfStd,
                Message = d.NumOfStd >= 2 ? "overload" : "we need more std",
                Color = d.NumOfStd >= 2 ? "red" : "green"
            });
            return Ok(dto);
        }

   
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Department>> GetById(int id)
        {
            var d = await _db.Departments.FindAsync(id);
            if (d == null) return NotFound();
            return Ok(d);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Department dept)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Departments.Add(dept);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = dept.Id }, dept);
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(LocationFilter))] 
        public async Task<ActionResult> Update(int id, [FromBody] Department dept)
        {
            if (id != dept.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _db.Departments.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dept.Name;
            existing.Manager = dept.Manager;
            existing.Location = dept.Location;
            existing.NumOfStd = dept.NumOfStd;

            await _db.SaveChangesAsync();
            return NoContent();
        }

  
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var d = await _db.Departments.FindAsync(id);
            if (d == null) return NotFound();
            _db.Departments.Remove(d);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

