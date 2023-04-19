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
    public class AttendanceService : IAttendanceService
    {
        private readonly IGenericRepo<Attendance> _attendanceRepo;

        public AttendanceService(IGenericRepo<Attendance> attendanceRepo)
        {
            _attendanceRepo = attendanceRepo;
        }

        public async Task<Attendance> CreateAttendance(Attendance attendance)
        {
            try
            { 
                return await _attendanceRepo.CreateAttendance(attendance);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Attendance> GetAttendanceById(int id)
        {
          return await _attendanceRepo.GetAttendanceById(id);
        }

        public async Task<List<Attendance>> GetAll(int labId)
        {
            try
            {
                List<Attendance> attendances = await _attendanceRepo.GetAll();
                attendances = attendances.Where(a => a.LabId == labId).ToList();
                return attendances;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAttendance(int attId, int labId, int studId, bool present)
        {
            var attendance = await _attendanceRepo.GetAttendanceById(attId);
            if (attendance == null)
                throw new ArgumentException("Attendance not found");
            if (labId != null && studId != null && present != null)
            {
                attendance.AttId = attId;
                attendance.LabId = labId;
                attendance.StudId = studId;
                attendance.Present = present;
            }
            await _attendanceRepo.UpdateAttendance(attendance);
          
        }

        public async Task DeleteAttendance(int id)
        {
           await _attendanceRepo.DeleteAttendance(id);
        }
    }
}
