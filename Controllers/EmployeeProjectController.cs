using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onlineshop.Db;

using SibersTest.Models;
using SibersTest.Models.Request;

namespace SibersTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProjectController : ControllerBase
    {
        private readonly DbContextAPI _context;
       
       // private readonly object ordinal;

        public EmployeeProjectController(DbContextAPI context)
        {
            _context = context;
        }

        // GET: api/EmployeeProject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeProject>>> GetEmployeeProjects()
        {
            var emplProject = await _context.EmployeeProjects.ToListAsync();
          
            return emplProject;
        }

        // GET: api/EmployeeProject/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeProject>> GetEmployeeProject(int id)
        {
          if (_context.EmployeeProjects == null)
          {
              return NotFound();
          }
            var employeeProject = await _context.EmployeeProjects.FindAsync(id);

            if (employeeProject == null)
            {
                return NotFound();
            }

            return employeeProject;
        }

        // PUT: api/EmployeeProject/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeProject(int id, AddEmployeeProject addEmployeeProject)
        {
            var empl = await _context.EmployeeProjects.FindAsync(id);
            if (empl != null)
            {
                empl.ProjectId = addEmployeeProject.ProjectId;
              
               
              
                await _context.SaveChangesAsync();
            }
               
           
            return NoContent();
        }

        // POST: api/EmployeeProject
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeProject>> PostEmployeeProject(AddEmployeeProject addEmployeeProject)
        {
            EmployeeProject empl = new EmployeeProject();
            empl.ProjectId = addEmployeeProject.ProjectId;
            empl.EmployeeId = addEmployeeProject.EmployeeId;
            await _context.EmployeeProjects.AddAsync(empl);
            await _context.SaveChangesAsync();

            return empl;
        }

        // DELETE: api/EmployeeProject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeProject(int id)
        {
            if (_context.EmployeeProjects == null)
            {
                return NotFound();
            }
            var employeeProject = await _context.EmployeeProjects.FindAsync(id);
            if (employeeProject == null)
            {
                return NotFound();
            }

            _context.EmployeeProjects.Remove(employeeProject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeProjectExists(int id)
        {
            return (_context.EmployeeProjects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
