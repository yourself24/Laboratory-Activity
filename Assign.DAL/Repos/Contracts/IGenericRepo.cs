using System.Net.Mime;

namespace Assign.DAL.Repos.Contracts;
using Assign.DAL.Repos.Contracts;
using Assign.DAL.Models;

public interface IGenericRepo<TModel> where TModel: class
{
    //student methods 
    Task<List<TModel>> GetAll();
    Task<Student> GetStudentById(int id);
    Task<Student> CreateStudent(Student student);
    Task UpdateStudent(Student student);
    Task DeleteStudent(int id);
    Student GetStudentByName(string name);
    //attendance methods
    Task<Attendance> CreateAttendance(Attendance attendance);
    Task<Attendance> GetAttendanceById(int id);
    //Task<List<Attendance>> GetAllAttendances();
    Task UpdateAttendance(Attendance attendance);
    Task DeleteAttendance(int id);
    //lab methods
    Task<Lab> CreateLab(Lab lab);
    Task<Lab> GetLabById(int id);
    Task<List<Lab>> GetAllLabs();
    Task UpdateLab(Lab lab);
    Task DeleteLab(int id);
    //teacher methods
    Task<List<Teacher>> GetAllTeachers();
    Teacher GetTeacherByName(string name);
    //submission methods
    Task<Submission> CreateSubmission(Submission submission);
    Task<Submission> GetSubmissionById(int id);
    Task<List<Submission>> GetAllSubmissions();
    Task UpdateSubmission(Submission submission);
    Task DeleteSubmission(int id);
    //assignment methods
    Task<Assignment> CreateAssignment(Assignment assignment);
    Task<Assignment> GetAssignmentById(int id);
    Task<List<Assignment>> GetAllAssignments();
    Task UpdateAssignment(Assignment assignment);
    Task DeleteAssignment(int id);
    //token methods
    Task<Token> CreateToken(Token token);
    Task DeleteToken(int id);
    Token GetTokenByString(string token11);
    //Task<List<Token>> GetAllTokens();
    
    
}