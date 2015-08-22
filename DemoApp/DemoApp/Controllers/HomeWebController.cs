﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace DemoApp.Controllers
{
    public class HomeWebController : Controller
    {
        public static Dictionary<string,string> IpDictionary = new Dictionary<string, string>(); 
        public ActionResult Index()
        {
            var ipAddress = Request.UserHostAddress;
            var username = LookupUsername(ipAddress);
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login");
            }
            var userInfo = new UserInfo { IpAddress = ipAddress,Username = username};
            return View(userInfo);
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(string name)
        {
            var ipAddress = Request.UserHostAddress;

            if (IpDictionary.ContainsKey(ipAddress))
            {
                IpDictionary.Remove(ipAddress);
            }
            IpDictionary.Add(Request.UserHostAddress, name);
            return RedirectToAction("Index");
        }

        private string LookupUsername(string ipAddress)
        {
            if (IpDictionary.ContainsKey(ipAddress))
            {
                return IpDictionary[ipAddress];
            }

            return null;
        }
    }

    public class UserInfo
    {
        public string Username { get; set; }
        public string IpAddress { get; set; }
    }
}