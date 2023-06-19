using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Onlineshop.Db;
using SibersTest.Models;
using SibersTest.Models.Request;

namespace SibersTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DbContextAPI _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public EmployeeController(DbContextAPI context, IWebHostEnvironment environment, IConfiguration config)
        {
            _context = context;
            _webHostEnvironment = environment;
            _config = config;
        }

        private string GetImg(string userName)
        {
            var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
            var contents = provider.GetDirectoryContents(Path.Combine("Uploads", "User"));
            var objFiles = contents.OrderBy(m => m.LastModified).ToArray();

            var obPng = objFiles.FirstOrDefault(x => x.Name.Contains(userName + ".png"));
            var obJpg = objFiles.FirstOrDefault(x => x.Name.Contains(userName + ".jpg"));
            var obJpeg = objFiles.FirstOrDefault(x => x.Name.Contains(userName + ".jpeg"));
            if (obPng != null)
            {
                return _config.GetValue<string>("Kestrel:Endpoints:Http:Url") + "/Uploads/User/" +
                       obPng.Name;
            }
            else if (obJpg != null)
            {
                return _config.GetValue<string>("Kestrel:Endpoints:Http:Url") + "/Uploads/User/" +
                       obJpg.Name;
            }
            else if (obJpeg != null)
            {
                return _config.GetValue<string>("Kestrel:Endpoints:Http:Url") + "/Uploads/User/" +
                       obJpeg.Name;
            }
            else
            {
                return _config.GetValue<string>("Kestrel:Endpoints:Http:Url") + "/Uploads/User/loader.png";
            }
        }

        // GET: api/Employee
        [HttpGet("getAllEmployee"),Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
         
            var employees = await _context.Employees.Include(x => x.EmployeeProjects).Include(x => x.Tasks).ToListAsync();
          foreach (var e in employees)
            {
                e.linkImg = GetImg(e.FullName);
            }
            return employees;
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            employee.linkImg = GetImg(employee.FullName);
            return employee;
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, AddEmployee addEmployee)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                employee.FullName = addEmployee.FullName;
               
                employee.Email = addEmployee.Email;
                employee.PositionTitle = addEmployee.PositionTitle;
              
                employee.Mobile = addEmployee.Mobile;
                employee.linkImg = GetImg(addEmployee.FullName);
                await _context.SaveChangesAsync();
               
                return Ok(employee);
            }
          

           
               
           
            return NoContent();
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(AddEmployee addEmployee)
        {
            Employee employee = new Employee();
            employee.FullName = addEmployee.FullName;
           
            employee.Email = addEmployee.Email;
            employee.PositionTitle = addEmployee.PositionTitle;
           
            employee.Mobile = addEmployee.Mobile;
            employee.linkImg = GetImg(addEmployee.FullName);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
