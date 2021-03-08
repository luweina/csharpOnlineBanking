using Hw1.Data;
using Hw1.Repositories;
using Hw1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hw1.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;
        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index( string accountType)
        {
            ViewBag.header = $"{ accountType} Account Summary";
            ClientInfoVMRepo ciVM = new ClientInfoVMRepo(_context);
            var query = accountType == "All" ? ciVM.GetAll() : ciVM.GetAll().Where(x => x.AccountType == accountType) ;
            var sort = query.OrderBy(p => p.LastName);
            return View(sort);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int clientId, int accountNum)
        {
            ViewBag.Title = "Accounts Details";
            ClientInfoVMRepo ciVM = new ClientInfoVMRepo(_context);
            var query = ciVM.Get(clientId, accountNum);
            return View(query);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int clientId, int accountNum)
        {
            ClientInfoVMRepo ciVM = new ClientInfoVMRepo(_context);
            var query = ciVM.Get(clientId, accountNum);
            return View(query);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(ClientInfoVM clinetInfoVM)
        {
            string email = User.Identity.Name;
            
            ClientInfoVMRepo ciVM = new ClientInfoVMRepo(_context);
            ciVM.Update(clinetInfoVM);
            return RedirectToAction("Details", "Admin", new { clientId = clinetInfoVM.ClientId, accountNum = clinetInfoVM.AccountNum });

        }


    }
}
