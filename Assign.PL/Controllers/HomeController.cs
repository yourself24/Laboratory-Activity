using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assign.PL.Models;
using Assign.BLL.Services.Contracts;
using Assign.DAL.Models;

namespace Assign.PL.Controllers;

public class HomeController : Controller
{
    private readonly IStudentService _studentService;

    public HomeController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public async Task<IActionResult> ShowStudents()
    {
        var students = await _studentService.GetAll();
        return View(students);
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}