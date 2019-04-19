using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gusto.Class;
using GustoLib.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gusto.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaseAdminController : Controller
    {
        public readonly GustoDbContext _context;

        public BaseAdminController(GustoDbContext context)
        {
            _context = context;
        }

        protected void DisplayMessage(string message, TypeMessage typeMessage)
        {
            TempData["Message"] = JsonConvert.SerializeObject(new FlashMessage(message, typeMessage));
        }
    }
}