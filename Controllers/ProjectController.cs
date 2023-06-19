using System;
using System.Collections.Generic;
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
    public class ProjectController : ControllerBase
    {
        private readonly DbContextAPI _context;

        public ProjectController(DbContextAPI context)
        {
            _context = context;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
         
            return await _context.Projects.Include(x=>x.EmployeeProjects).Include(x=>x.Tasks).ToListAsync();
        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
          if (_context.Projects == null)
          {
              return NotFound();
          }
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/Project/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, AddProject addProject)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project != null) {
                project.ProjectName = addProject.ProjectName;
                project.ProjectManagerId = addProject.ProjectManagerId;
                project.ClientCompany = addProject.ClientCompany;
                project.ExecutingCompany = addProject.ExecutingCompany;
                var startDate = DateTime.Parse(addProject.StartDate);
                var endDate = DateTime.Parse(addProject.EndDate);
                project.StartDate = startDate;
                project.EndDate = endDate;
                project.Priority = addProject.Priority;
                await _context.SaveChangesAsync();
                return Ok(project);
            }
            return Ok(NotFound("Not found"));
        }

        // POST: api/Project
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(AddProject addProject)
        {
            Project project = new Project();
            project.ProjectName = addProject.ProjectName;
            project.ProjectManagerId = addProject.ProjectManagerId;
            project.ClientCompany = addProject.ClientCompany;
            project.ExecutingCompany = addProject.ExecutingCompany;
            var startDate = DateTime.Parse(addProject.StartDate);
            var endDate = DateTime.Parse(addProject.EndDate);
            project.StartDate = startDate.ToUniversalTime();
            project.EndDate = endDate.ToUniversalTime();
            project.Priority = addProject.Priority;
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
