using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMvc.Results;
using SimpleMvc.Test.TestViews.TestController;

namespace SimpleMvc.Test.TestControllers
{
    [TestClass]
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
            return View("EditUser", new TestModel());
        }

        public ActionResult User(string userName, string password)
        {
            User2Called = true;
            return View("EditUser", new TestModel());
        }


        [TestMethod]
        public void CallView()
        {
            Assert.Inconclusive("Not implemented!");
        }


        [TestMethod]
        public void CallViewWithNullViewName()
        {
            Assert.Inconclusive("Not implemented!");
        }


        [TestMethod]
        public void CallViewWithNullModel()
        {
            Assert.Inconclusive("Not implemented!");
        }


        [TestMethod]
        public void CallRedirectWithNullActionName()
        {
            Assert.Inconclusive("Not implemented!");
        }


        [TestMethod]
        public void CallRedirectWithNullControllerName()
        {
            Assert.Inconclusive("Not implemented!");
        }


        [TestMethod]
        public void CallRedirectWithNullValues()
        {
            Assert.Inconclusive("Not implemented!");
        }


    }
}
