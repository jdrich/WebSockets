﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSockets.WebSockets;

namespace WebSockets.Controllers
{
    public class MainController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}