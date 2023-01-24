using Bilet5.DAL;
using Bilet5.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace Bilet5.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Employees= _context.Employees
            };
            return View(homeVM);
        }
    }
}