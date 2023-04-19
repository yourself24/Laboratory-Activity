using Assign.DAL.Models;

namespace Assign.BLL.Services.Contracts;

public interface ISubmissionService
{
    Task<List<Submission>> GetAll(int studentId);
    Task<Submission> CreateSubmission(Submission submission);
    Task<Submission> GetSubmissionById(int id);
    Task UpdateSubmission(int id, int grade);
    Task DeleteSubmission(int id);
}