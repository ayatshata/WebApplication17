using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;
using WebApplication17.DTOs;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _db;
        public EmployeeController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var emps = await _db.Employees.AsNoTracking().ToListAsync();
            var dto = emps.Select(e => new EmployeeDto
            {
                Name = e.Name,
                Dept = e.Dept,
                Manager = e.Manager,
                Date = e.Date.ToShortDateString(),
                Skills = e.Skills
            });
            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var e = await _db.Employees.FindAsync(id);
            if (e == null) return NotFound();
            return Ok(e);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Employee emp)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Employees.Add(emp);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = emp.Id }, emp);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, Employee emp)
        {
            if (id != emp.Id) return BadRequest();

            var existing = await _db.Employees.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = emp.Name;
            existing.Dept = emp.Dept;
            existing.Manager = emp.Manager;
            existing.Date = emp.Date;
            existing.Skills = emp.Skills;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var e = await _db.Employees.FindAsync(id);
            if (e == null) return NotFound();
            _db.Employees.Remove(e);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
