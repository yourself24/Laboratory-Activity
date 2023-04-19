namespace Assign.BLL.Services;
using Assign.DAL.Models;
using Assign.DAL.Repos.Contracts;
using Assign.BLL.Services.Contracts;
using BCrypt.Net;
public class StudentService:IStudentService
{
    private readonly IGenericRepo<Student> _studentRepo;
    public StudentService(IGenericRepo<Student> studentRepo)
    {
        _studentRepo = studentRepo;
    }

    public async Task<List<Student>> GetAll()
    {
        try
        {
            return await _studentRepo.GetAll();
        }
        catch
        {
            throw;
        }
    }

    public bool Login(string username, string password)
    {
      var student = _studentRepo.GetStudentByName(username);
        if (student == null)
        {
            return false;
        }
        else
        {
           
            if (BCrypt.Verify(password,student.Password))
            {
            return true;
            }
            else
            {
            return false;
            }
        }
    }

    public async Task<Student> SignUp(Student student)
    {
        try
        {
            string salt = BCrypt.HashPassword(student.Password);
            student.Password = salt;
            return await _studentRepo.CreateStudent(student);
        }
        catch
        {
            throw;
        }
    }

    public async Task<Student> GetStudentById(int id)
    {
        return await _studentRepo.GetStudentById(id);
    }

    public async Task UpdateStudent(int id, string name, string email, string password, int group_no, string hobby)
    {
        var stud = await _studentRepo.GetStudentById(id);
        if (stud == null)
        {
            throw new ArgumentException($"Student with id {id} does not exist");
        }
        stud.Sid = id;
        stud.Name = name;
        stud.Email = email;
        stud.Password = BCrypt.HashPassword(password);
        stud.GroupNo = group_no;
        stud.Hobby = hobby;
        await _studentRepo.UpdateStudent(stud);
    }

    public async Task DeleteStudent(int id)
    {
        await _studentRepo.DeleteStudent(id);
    }

    public Student GetStudentByName(string name)
    {
        return  _studentRepo.GetStudentByName(name);
    }
}