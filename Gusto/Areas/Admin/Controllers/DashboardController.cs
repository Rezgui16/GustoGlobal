using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GustoLib.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gusto.Areas.Admin.Controllers
{
    [Authorize(Roles = "Chef,Admin")]
    public class DashboardController : BaseAdminController
    {
        public DashboardController(GustoDbContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}