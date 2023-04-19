using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Assign.BLL.Services.Contracts;
using Assign.DAL.DataContext;
using Assign.DAL.Models;
using Assign.PL.Models;


namespace Assign.PL.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly ITokenService _tokensService;
        private readonly SdlabContext _context;

        public LoginController(ITeacherService teacherService, IStudentService studentService, ITokenService tokensService,SdlabContext context)
        {
            _teacherService = teacherService;
            _studentService = studentService;
            _tokensService = tokensService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
          var model = new LoginViewModel();
          return View(model);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Role == "Student")
                {
                    var student = _studentService.GetStudentByName(model.Email);
                    var token = _context.Tokens.FirstOrDefault(t => t.Token1 == model.Password);
                    if (token != null && token.Used == false)
                    {
                        token.Used = true;
                        Console.WriteLine(token);
                        _context.Tokens.Update(token);
                        _context.SaveChanges();
                        string redir = "Edit/" + student.Sid;
                        return Redirect("/Student/" + redir);
                    }
                   
                    if (student != null && _studentService.Login(model.Email, model.Password))
                    {
                        return RedirectToAction("IndexStudents", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError("","Invalid email or password");
                    }
                }
                else if (model.Role == "Teacher")
                {
                    var teacher = _teacherService.GetTeacherByName(model.Email);
                    if (teacher != null && _teacherService.SignTeacherIn(model.Email, model.Password))
                    {
                        return RedirectToAction("IndexTeachers", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid email or password");
                    }
                }
                
            }
            return View(model);
        }

        public IActionResult IndexStudents()
        {
            return View();
        }

        public IActionResult IndexTeachers()
        {
            return View();
        }
    }
}