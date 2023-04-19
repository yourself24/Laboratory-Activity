using Assign.DAL.DataContext;
using Assign.DAL.Repos;
using Assign.DAL.Repos.Contracts;
using Assign.DAL.Models;
using Assign.BLL.Services;
using Assign.BLL.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

    builder.Services.AddEntityFrameworkNpgsql().AddEntityFrameworkNpgsql().
        AddDbContext<SdlabContext>
            (opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresBoi")));



builder.Services.AddTransient(typeof(IGenericRepo<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ILabService, LabService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddScoped<IAssignService, AssignService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ITokenService, TokenService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();