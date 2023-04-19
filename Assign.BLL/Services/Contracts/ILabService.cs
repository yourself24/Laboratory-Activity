using System.Runtime.InteropServices.JavaScript;
using Assign.DAL.Models;

namespace Assign.BLL.Services.Contracts;

public interface ILabService
{
    Task<List<Lab>> GetAll();
    Task<Lab> CreateLab(Lab lab);
    Task UpdateLab(int labId,int labNo,DateTime date,string title,string description);
    Task<Lab> FindLabById(int id);
    Task DeleteLab(int id);
}