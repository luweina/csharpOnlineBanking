using Hw1.Data;
using Hw1.Models;
using Hw1.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hw1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        [Authorize]
        public ActionResult Index()
        {
            
            return View();
        }
        [Authorize]
        public ActionResult SecureArea()
        {
            string email = User.Identity.Name;
            ClientInfoVMRepo ciVM = new ClientInfoVMRepo(_context);
            var query = ciVM.GetByEmail(email);
            return View(query);
        }


     

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
