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
using SibersTest.Models.Response;

namespace SibersTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly DbContextAPI _context;

        public TaskController(DbContextAPI context)
        {
            _context = context;
        }

        // GET: api/Task
        [HttpGet("/getTaskByStatus")]
        public async Task<IActionResult> GetTasksBy(StatusTask status)
        { 
            var responses = new List<GetTaskResponse>();
            var tasks = await (from t1 in _context.Tasks where t1.Status==status
                               select new {id=t1.Id,name=t1.Name,
                                   author=t1.Author.FullName,
                                   project=t1.Project.ProjectName, 
                                  executor=t1.Executor.Employee.FullName,
                                   description=t1.Description,
                                   status=t1.Status }).OrderByDescending(x=>x.id)
                .ToListAsync();
            foreach(var t in tasks)
            {
                GetTaskResponse response = new GetTaskResponse();
                response.Name = t.name;
                response.Author = t.author;
                response.Project = t.project;
                response.Executor = t.executor;
                response.Description = t.description;
                response.Status = t.status.ToString();
                responses.Add(response);
            }
            return Ok(responses);
        }

        [HttpGet]
        public async Task<ActionResult<Models.Task>> GetTasks(int page,int limit)
        {
            var responses = new List<GetTaskResponse>();
            var tasks = await (from t1 in _context.Tasks
                               select new
                               {
                                   id = t1.Id,
                                   name = t1.Name,
                                   author = t1.Author.FullName,
                                   project = t1.Project.ProjectName,
                                   executor = t1.Executor.Employee.FullName,
                                   description = t1.Description,
                                   status = t1.Status
                               }).Skip((page-1)*limit)
                               .Take(limit)
                .ToListAsync();
            foreach (var t in tasks)
            {
                GetTaskResponse response = new GetTaskResponse();
                response.Name = t.name;
                response.Author = t.author;
                response.Project = t.project;
                response.Executor = t.executor;
                response.Description = t.description;
                response.Status = t.status.ToString();
                responses.Add(response);
            }
            return Ok(responses);

        }
            // GET: api/Task/5
            [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(int id)
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // PUT: api/Task/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, UpdateTask addTask)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.AuthorId = addTask.AuthorId;
                task.Status = addTask.Status;
                task.Description = addTask.Description;
                task.Name = addTask.Name;
                task.ProjectId = addTask.ProjectId;
                task.ExecutorId = addTask.ExecutorId;
               
                await _context.SaveChangesAsync();
                return Ok(task);
            }
            return NoContent();
        }

        // POST: api/Task
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(AddTask addTask)
        {
            var authorId = await (from e in _context.Employees where e.PositionTitle == "ProjectManager" select e.Id).FirstOrDefaultAsync();
            Models.Task task = new Models.Task();
            task.AuthorId = authorId;
            task.Status = addTask.Status;
            task.Description = addTask.Description;
            task.Name = addTask.Name;
            task.ProjectId = addTask.ProjectId;
            task.ExecutorId = addTask.ExecutorId;
           
            
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        // DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
          
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("/deleteTaskInProject")]
        public async Task<IActionResult> DeleteTaskInProject(string project,string task)
        {

            var t = await (from t1 in _context.Tasks 
                           where t1.Project.ProjectName.ToUpper().Equals(project.ToUpper())&&t1.Name.ToLower().Equals(task.ToLower()) 
                           select t1)
                           .FirstOrDefaultAsync();
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(t);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return (_context.Tasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
