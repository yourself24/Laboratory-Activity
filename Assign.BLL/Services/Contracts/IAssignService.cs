using Assign.DAL.Models;

namespace Assign.BLL.Services.Contracts;

public interface IAssignService
{
    Task<Assignment> CreateAssignment(Assignment assignment);
    Task<List<Assignment>> GetAll(int labId);
    Task<Assignment> GetAssignmentById(int id);
    Task UpdateAssignment(int id, int labId, string name, DateOnly deadline, string description);
    Task DeleteAssignment(int id);
}