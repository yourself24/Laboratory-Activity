using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assign.BLL.Services.Contracts;
using Assign.DAL.Models;
using Assign.DAL.Repos.Contracts;

namespace Assign.BLL.Services
{
    public class AssignService : IAssignService
 
    {
        private readonly IGenericRepo<Assignment> _assignRepo;

        public AssignService(IGenericRepo<Assignment> assignRepo)
        {
            _assignRepo = assignRepo;
        }
        public async Task<Assignment> CreateAssignment(Assignment assignment)
        {
            try
            {
                return await _assignRepo.CreateAssignment(assignment);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Assignment>> GetAll(int labId)
        {
            try
            {
                List<Assignment> assignments = await _assignRepo.GetAll();
                assignments = assignments.Where(a => a.Lid == labId).ToList();
                return assignments;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Assignment> GetAssignmentById(int id)
        {
           return await _assignRepo.GetAssignmentById(id);
        }

        public async Task UpdateAssignment(int id, int labId, string name, DateOnly deadline, string description)
        {
            var assign = await _assignRepo.GetAssignmentById(id);
            if (assign == null)
                throw new ArgumentException("Assignment not found");
            if (labId != null && name != null && deadline != null && description != null)
            {
                assign.Asid = id;
                assign.Lid = labId;
                assign.AsName= name;
                assign.Deadline = deadline;
                assign.AsDesc = description;
                
            }
            await _assignRepo.UpdateAssignment(assign);
        }

        public async Task DeleteAssignment(int id)
        {
             await _assignRepo.DeleteAssignment(id);
        }
    }
}
