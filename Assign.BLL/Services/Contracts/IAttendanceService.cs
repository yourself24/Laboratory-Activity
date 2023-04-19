using Assign.DAL.Models;

namespace Assign.BLL.Services.Contracts;

public interface IAttendanceService
{
    Task<Attendance> CreateAttendance(Attendance attendance);
    Task<Attendance> GetAttendanceById(int id);
    Task<List<Attendance>> GetAll(int labId);
    Task UpdateAttendance(int attId,int labId,int studId,bool present);
    Task DeleteAttendance(int id);
    
}