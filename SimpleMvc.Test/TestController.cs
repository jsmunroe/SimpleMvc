using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMvc.Results;

namespace SimpleMvc.Test
{
    public class TestController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult User(int id)
        {
            return View("EditUser");
        }

        public ActionResult User(string userName, string password)
        {
            return View("EditUser");
        }
    }
}
