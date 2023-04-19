using Assign.BLL.Services.Contracts;
using Assign.DAL.Models;
using Assign.DAL.Repos.Contracts;

namespace Assign.BLL.Services;

public class TeacherService: ITeacherService
{
    private readonly IGenericRepo<Teacher> _teacherRepository;
    public TeacherService(IGenericRepo<Teacher> teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    public async Task<List<Teacher>> GetAll()
    {
        try
        {
            return await _teacherRepository.GetAll();
        }
        catch
        {
            throw;
        }
    }
    public Teacher GetTeacherByName(string name)
    {
        return _teacherRepository.GetTeacherByName(name);
    }

    public bool SignTeacherIn(string username, string password)
    {
        var teacher = _teacherRepository.GetTeacherByName(username);
        if(teacher == null)
        {
            return false;
        }
        else
        {
            if (BCrypt.Net.BCrypt.Verify(password, teacher.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }

  
}