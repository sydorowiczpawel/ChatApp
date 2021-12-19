using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChatApp.Models;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string SessionKey = "SESSION_KEY";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            var login = HttpContext.Session.GetString(SessionKey);
            
            if (login != null)
            {
                return RedirectToAction("ChatApp");
            }

            var user = new UserModel();
            return View(user);
        }
        
        [HttpPost]
        public IActionResult Login(UserModel uModel)
        {
            if (!ModelState.IsValid)
            {
                return View(uModel);
            }
            
            HttpContext.Session.SetString(SessionKey, uModel.Login);
            return RedirectToAction("ChatApp");
        }
        
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey);
            return RedirectToAction("Login");
        }
        
        public IActionResult ChatApp()
        {
            var userLogin = HttpContext.Session.GetString(SessionKey);

            if (userLogin == null)
            {
                return RedirectToAction("Login");
            }
            
            var viewModel = new UserModel()
            {
                Login = userLogin
            };
            
            return View(viewModel);
        }

            public IActionResult Register()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}