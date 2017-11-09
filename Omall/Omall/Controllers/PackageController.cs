using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace Omall.Controllers
{
    public class PackageController : Controller
    {
        // GET: Package
        public ActionResult Index()
        {
            PackageDAL PackageDAL = new PackageDAL();
            var defPkges = PackageDAL.GetPackageDefaultsDetails();

            return View(defPkges);
        }
    }
}