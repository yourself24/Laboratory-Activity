
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;
namespace WebApplication1.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TeacherController : ControllerBase
{
    //this class will be used to handle the requests from the client for the teacher table
    private readonly DbCtxt _context;
    public TeacherController(DbCtxt context)
    {
        this._context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
    {
        return await _context.Teacher.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Teacher>> GetTeacher(int id)
    {
        var teacher = await _context.Teacher.FindAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }
        return teacher;
    }
    [HttpGet("testdb")]
    public async Task<ActionResult<string>> TestDatabaseConnection()
    {
        try
        {
            await _context.Database.OpenConnectionAsync();
            var dbVersion = _context.Database.GetDbConnection().ServerVersion;
            await _context.Database.CloseConnectionAsync();
            return $"Connected to database. Version: {dbVersion}";
        }
        catch (Exception ex)
        {
            return $"Error connecting to database: {ex.Message}";
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
    {
        if (id!=teacher.tid){
            return BadRequest();
        }
        _context.Entry(teacher).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();


        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TeacherExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    private bool TeacherExists(int id)
    {
        return _context.Teacher.Any(e => e.tid == id);
    }
    [HttpPost]
    public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
    {
        _context.Teacher.Add(teacher);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetTeacher", new { id = teacher.tid }, teacher);
    }

    


}