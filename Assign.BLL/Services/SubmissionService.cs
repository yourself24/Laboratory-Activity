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
    public class SubmissionService: ISubmissionService
    {
        private readonly IGenericRepo<Submission> _submissionRepo;

        public SubmissionService(IGenericRepo<Submission> submissionRepo)
        {
            _submissionRepo = submissionRepo;
        }
        public async Task<List<Submission>> GetAll(int studId)
        {
            try
            {
                List<Submission> submissions = await _submissionRepo.GetAll();
                submissions = submissions.Where(s => s.StudId == studId).ToList();
                return submissions;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Submission> CreateSubmission(Submission submission)
        {
            try
            {
                return await _submissionRepo.CreateSubmission(submission);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Submission> GetSubmissionById(int id)
        {
           return await _submissionRepo.GetSubmissionById(id);
        }

        public async Task UpdateSubmission(int id,int grade)
        {
            var subm = await _submissionRepo.GetSubmissionById(id);
            if (subm == null)
                throw new ArgumentException("Submission not found");
            if (grade != null)
            {
                subm.Grade = grade;
            }
            await _submissionRepo.UpdateSubmission(subm);
        }


        public async Task DeleteSubmission(int id)
        {
            await _submissionRepo.DeleteSubmission(id);
        }
    }
}
