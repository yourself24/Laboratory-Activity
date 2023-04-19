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
    public class LabService : ILabService
    
    {
        private readonly IGenericRepo<Lab> _labRepo;

        public LabService(IGenericRepo<Lab> labRepo)
        {
            _labRepo = labRepo;
        }

        public async Task<List<Lab>> GetAll()
        {
            try
            {
                return await _labRepo.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Lab> CreateLab(Lab lab)
        {
            try
            {
                return await _labRepo.CreateLab(lab);
            }
            catch
            {
                throw;
            }

        }

        public async Task UpdateLab(int labId, int labNo, DateTime date, string title, string description)
        {
            var labob = await _labRepo.GetLabById(labId);
            if(labob ==null)
                throw new ArgumentException("Lab not found");
            if (labNo != null && date != null && title != null && description != null)
            {
                labob.LabNo = labNo;
                labob.Date = date;
                labob.Title = title;
                labob.Description = description;
            }

            await _labRepo.UpdateLab(labob);

        }

        public async Task<Lab> FindLabById(int id)
        {
            return await _labRepo.GetLabById(id);
        }

        public async Task DeleteLab(int id)
        {
            await _labRepo.DeleteLab(id);
        }
    }
}
