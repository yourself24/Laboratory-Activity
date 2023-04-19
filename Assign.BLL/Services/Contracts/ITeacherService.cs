    using Assign.DAL.Models;

    namespace Assign.BLL.Services.Contracts;

    public interface ITeacherService
    {
        Task<List<Teacher>> GetAll();
        public Teacher GetTeacherByName(string name);

        public bool SignTeacherIn(string username, string password);
    }