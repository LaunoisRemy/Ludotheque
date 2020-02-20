using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ludotheque.Controllers
{
    public class JeuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        // GET: /HelloWorld/Jeu
        public string Welcome(string name, int ID = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        }
    }
}