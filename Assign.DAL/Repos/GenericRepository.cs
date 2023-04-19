using System.Net.Mime;
using Assign.DAL.DataContext;
using Assign.DAL.Models;
using Assign.DAL.Repos.Contracts;
using Microsoft.EntityFrameworkCore;
namespace Assign.DAL.Repos;


public class GenericRepository<TModel> : IGenericRepo<TModel> where TModel : class
{
    private readonly SdlabContext _context;
    private readonly DbSet<TModel> _dbSet;

    public GenericRepository(SdlabContext context)
    {
        _context = context;
        _dbSet = context.Set<TModel>();
    }

    public async Task<List<TModel>> GetAllStudents()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch
        {
            throw;
        }
    }

    public async Task<List<TModel>> GetAll()
    {
        try 
        {
            return await _dbSet.ToListAsync();
        }
        catch
        {
            throw;
        }
    }

    public async Task<Student> GetStudentById(int id)
    {
        try
        {
            return await _context.Students.FindAsync(id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<Student> CreateStudent(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            throw new ArgumentException($"NO student with this id: {id}");
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }
    public Student GetStudentByName(string name)
    {
        return _context.Students.FirstOrDefault(student => student.Email == name);
    }

    public async Task<Attendance> CreateAttendance(Attendance attendance)
    {
        await _context.Attendances.AddAsync(attendance);
        await _context.SaveChangesAsync();
        return attendance;
    }

    public async Task<Attendance> GetAttendanceById(int id)
    {
        try
        {
            return await _context.Attendances.FindAsync(id);
        }
        catch
        {
            throw;
        }
    }

    public async Task UpdateAttendance(Attendance attendance)
    {
        _context.Attendances.Update(attendance);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAttendance(int id)
    {
        var attendance = await _context.Attendances.FindAsync(id);
        if (attendance == null)
        {
            throw new ArgumentException($"NO attendance with this id: {id}");
        }

        _context.Attendances.Remove(attendance);
        await _context.SaveChangesAsync();
    }

    public async Task<Lab> CreateLab(Lab lab)
    {
        await _context.Labs.AddAsync(lab);
        await _context.SaveChangesAsync();
        return lab;
    }

    public async Task<Lab> GetLabById(int id)
    {
        try
        {
            return await _context.Labs.FindAsync(id);
        }
        catch
        {
            throw;
        }
    }

    public Task<List<Lab>> GetAllLabs()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateLab(Lab lab)
    {
        _context.Labs.Update(lab);
        await _context.SaveChangesAsync();
        
    }

    public async Task DeleteLab(int id)
    {
        var lab = _context.Labs.Find(id);
        if (lab == null)
        {
            throw new ArgumentException($"NO lab with this id: {id}");
        }
        _context.Labs.Remove(lab);
        await _context.SaveChangesAsync();
    }

    
    public Task<List<Teacher>> GetAllTeachers()
    {
        throw new NotImplementedException();
    }
    
    public  Teacher GetTeacherByName(string name)
    {
        return _context.Teachers.FirstOrDefault(teacher=> teacher.Email == name);
    }

    public async Task<Submission> CreateSubmission(Submission submission)
    {
        await _context.Submissions.AddAsync(submission);
        await _context.SaveChangesAsync();
        return submission;
    }

    public async Task<Submission> GetSubmissionById(int id)
    {
        try
        {
            return await _context.Submissions.FindAsync(id);
        }
        catch
        {
            throw;
        }
    }

    public Task<List<Submission>> GetAllSubmissions()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateSubmission(Submission submission)
    {
        _context.Submissions.Update(submission);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSubmission(int id)
    {
        var submission = await _context.Submissions.FindAsync(id);
        if (submission == null)
        {
            throw new ArgumentException($"NO submission with this id: {id}");
        }
        _context.Submissions.Remove(submission);
        await _context.SaveChangesAsync();
    }

    public async Task<Assignment> CreateAssignment(Assignment assignment)
    {
        await _context.Assignments.AddAsync(assignment);
        await _context.SaveChangesAsync();
        return assignment;
    }

    public async Task<Assignment> GetAssignmentById(int id)
    {
        try
        {
            return await _context.Assignments.FindAsync(id);

        }catch
        {
            throw;
        }
    }

    public Task<List<Assignment>> GetAllAssignments()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAssignment(Assignment assignment)
    {
        _context.Assignments.Update(assignment);
        await _context.SaveChangesAsync();
    }
    

    public async Task DeleteAssignment(int id)
    {
        var assign = await _context.Assignments.FindAsync(id);
        if (assign == null)
        {
            throw new ArgumentException($"NO assignment with this id: {id}");
        }
        _context.Assignments.Remove(assign);
        await _context.SaveChangesAsync();
    }

    public async Task<Token> CreateToken(Token token)
    {
        await _context.Tokens.AddAsync(token);
        await _context.SaveChangesAsync();
        return token;
    }

    public Token GetTokenByString(string token11)
    {
       return _context.Tokens.FirstOrDefault(token => token.Token1 == token11);
    }

    public async Task DeleteToken(int id)
    {
        var token = await _context.Tokens.FindAsync(id);
        if (token == null)
        {
            throw new ArgumentException($"NO token with this id: {id}");
        }
        _context.Tokens.Remove(token);
        await _context.SaveChangesAsync();
    }
}

    