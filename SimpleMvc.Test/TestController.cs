using SimpleMvc.Results;
using System;

namespace SimpleMvc.Test
{
    public class TestController : ControllerBase
    {
        public bool IndexCalled { get; private set; } = false;
        public bool User1Called { get; private set; } = false;
        public bool User2Called { get; private set; } = false;

        public ActionResult Index()
        {
            IndexCalled = true;
            return View();
        }

        public ActionResult User(int id)
        {
            User1Called = true;
            return View("EditUser", new TestUserModel());
        }

        public ActionResult User(string userName, string password)
        {
            User2Called = true;
            return View("EditUser", new TestUserModel());
        }
    }

    public class TestUserModel
    {

    }
}
