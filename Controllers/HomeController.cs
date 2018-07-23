using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginReg.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoginReg.Controllers
{
    public class HomeController : Controller
    {
        private UserContext _context;
        public HomeController(UserContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId != null)
            {
                return RedirectToAction("dashboard");
            }
            else
            {
                return View("Index");
            }
        }
        [HttpPost]
        [Route("new")]
        public IActionResult newUser(User newUser)
        {
            if (ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                newUser.confirm_password = Hasher.HashPassword(newUser,newUser.confirm_password);
                _context.Add(newUser);
                _context.SaveChanges();
                Console.WriteLine("Form Accepted.");
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Form was not Accepted.");
                return View("Index");
            }
        }
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email, string Password)
        {
            Console.WriteLine("Email is: " + Email);
            Console.WriteLine("Password is: " + Password);

            User checkUser = _context.user.SingleOrDefault(user => user.Email == Email);
            if (checkUser != null && Password != null)
            {
                var Hasher = new PasswordHasher<User>();
                if (0 != Hasher.VerifyHashedPassword(checkUser, checkUser.Password, Password))
                {
                    HttpContext.Session.SetInt32("UserId", checkUser.id);
                    return RedirectToAction("dashboard");
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

    }
}
