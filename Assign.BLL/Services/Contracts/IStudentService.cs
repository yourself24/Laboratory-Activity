namespace Assign.BLL.Services.Contracts;
using Assign.DAL.Models;


public interface IStudentService
{
    Task<List<Student>> GetAll();
    bool Login(string username, string password);
    Task<Student> SignUp(Student student);
    Task<Student> GetStudentById(int id);
    Task UpdateStudent(int id,string name, string email, string password,int group_no,string hobby);
    Task DeleteStudent(int id);
    Student GetStudentByName(string name);
}