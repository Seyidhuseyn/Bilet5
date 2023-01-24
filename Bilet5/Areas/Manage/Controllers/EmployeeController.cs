using Bilet5.DAL;
using Bilet5.Models;
using Bilet5.Utilies.Extension;
using Bilet5.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bilet5.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        readonly IWebHostEnvironment _env;
        readonly AppDbContext _context;
        public EmployeeController(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Employees.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeVM employeeVM)
        {
            IFormFile file = employeeVM.Image;
            string result = file?.CheckValidate("Image/", 300);
            if (result.Length>0)
            {
                ModelState.AddModelError("ImageUrl", result);
            }
            Employee employee = new Employee
            {
                FullName=employeeVM.FullName,
                Role=employeeVM.Role,
                Description=employeeVM.Description,
                Twitter=employeeVM.Twitter,
                Facebook=employeeVM.Facebook,
                Instagram=employeeVM.Instagram,
                LinkedIn=employeeVM.LinkedIn,
                ImageUrl = file.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "employee"))
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            Employee employee = _context.Employees.Find(id);
            if (employee == null) return NotFound();
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        { 
            if (id is null) return BadRequest();
            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return NotFound();
            UpdateEmployeeVM employeeVM= new UpdateEmployeeVM
            {
                FullName= employee.FullName,
                Role= employee.Role,
                Description= employee.Description,
                Twitter= employee.Twitter,
                Facebook= employee.Facebook,
                Instagram= employee.Instagram,
                LinkedIn= employee.LinkedIn
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id, UpdateEmployeeVM employeeVM)
        {
            IFormFile file = employeeVM.Image;
            string result = file?.CheckValidate("Image/", 300);
            if (result.Length > 0)
            {
                ModelState.AddModelError("ImageUrl", result);
            }
            Employee employee = new Employee
            {
                FullName = employeeVM.FullName,
                Role = employeeVM.Role,
                Description = employeeVM.Description,
                Twitter = employeeVM.Twitter,
                Facebook = employeeVM.Facebook,
                Instagram = employeeVM.Instagram,
                LinkedIn = employeeVM.LinkedIn,
                ImageUrl = file.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "employee"))
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
