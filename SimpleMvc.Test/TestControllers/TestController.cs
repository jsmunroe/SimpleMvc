using System;
using System.Linq;
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




        // Tests of ControllerBase abstract class.

        [TestMethod]
        public void CallView()
        {
            // Execute
            var viewResult = View();

            // Assert
            Assert.AreEqual(nameof(CallView), viewResult.ViewName);
            Assert.IsNull(viewResult.Model);
        }

        [TestMethod]
        public void CallViewWithViewName()
        {
            // Setup
            var testModel = new TestModel();

            // Execute
            var viewResult = View("ThisView", testModel);

            // Assert
            Assert.AreEqual("ThisView", viewResult.ViewName);
            Assert.AreSame(testModel, viewResult.Model);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallViewWithNullViewName()
        {
            // Setup
            var testModel = new TestModel();

            // Execute
            var viewResult = View(a_viewName: null, a_model: testModel);
        }


        [TestMethod]
        public void CallViewWithNullModel()
        {
            // Execute
            var viewResult = View(a_viewName: "ViewName", a_model: null);

            // Assert
            Assert.AreEqual("ViewName", viewResult.ViewName);
            Assert.IsNull(viewResult.Model);
        }


        [TestMethod]
        public void CallRedirect()
        {
            // Setup
            var values = new RouteDictionary();
            values["One"] = 1;
            values["Two"] = 2;

            // Execute
            var result = Redirect("ActionName", values);

            // Assert
            Assert.IsNull(result.ControllerName);
            Assert.AreEqual("ActionName", result.ActionName);

            Assert.IsNotNull(result.Values);
            Assert.IsTrue(result.Values.ContainsKey("One"));
            Assert.AreEqual(1, result.Values["One"]);
            Assert.IsTrue(result.Values.ContainsKey("Two"));
            Assert.AreEqual(2, result.Values["Two"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallRedirectWithNullActionName()
        {
            // Setup
            var values = new RouteDictionary();
            values["One"] = 1;
            values["Two"] = 2;

            // Execute
            var result = Redirect(a_actionName: null, a_values: values);
        }

        [TestMethod]
        public void CallRedirectWithNullValues()
        {
            // Execute
            var result = Redirect(a_actionName: "ActionName", a_values: null);

            // Assert
            Assert.IsNull(result.ControllerName);
            Assert.AreEqual("ActionName", result.ActionName);

            Assert.IsNotNull(result.Values);
            Assert.IsFalse(result.Values.Any());
        }

        [TestMethod]
        public void CallRedirectWithControllerName()
        {
            // Setup
            var values = new RouteDictionary();
            values["One"] = 1;
            values["Two"] = 2;

            // Execute
            var result = Redirect(a_actionName: "ActionName",a_controllerName: "Controller",  a_values: values);

            // Assert
            Assert.AreEqual("Controller", result.ControllerName);
            Assert.AreEqual("ActionName", result.ActionName);

            Assert.IsNotNull(result.Values);
            Assert.IsTrue(result.Values.ContainsKey("One"));
            Assert.AreEqual(1, result.Values["One"]);
            Assert.IsTrue(result.Values.ContainsKey("Two"));
            Assert.AreEqual(2, result.Values["Two"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallRedirectWithNullControllerName()
        {
            // Setup
            var values = new RouteDictionary();
            values["One"] = 1;
            values["Two"] = 2;
            
            // Execute
            var result = Redirect(a_actionName: "ActionName",a_controllerName: null,  a_values: values);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallRedirectWithControllerNameAndNullActionName()
        {
            // Setup
            var values = new RouteDictionary();
            values["One"] = 1;
            values["Two"] = 2;

            // Execute
            var result = Redirect(a_actionName: null, a_controllerName: "Controller", a_values: values);
        }

        [TestMethod]
        public void CallRedirectWithControllerNameAndNullValues()
        {
            // Execute
            var result = Redirect(a_actionName: "ActionName", a_controllerName: "Controller", a_values: null);

            // Assert
            Assert.AreEqual("Controller", result.ControllerName);
            Assert.AreEqual("ActionName", result.ActionName);

            Assert.IsNotNull(result.Values);
            Assert.IsFalse(result.Values.Any());
        }

        [TestMethod]
        public void CallRedirectWithDynamicValues()
        {
            // Setup
            dynamic values = new
            {
                One = 1,
                Two = 2,
            };

            // Execute
            var result = Redirect("ActionName", values);

            // Assert
            Assert.IsNull(result.ControllerName);
            Assert.AreEqual("ActionName", result.ActionName);

            Assert.IsNotNull(result.Values);
            Assert.IsTrue(result.Values.ContainsKey("One"));
            Assert.AreEqual(1, result.Values["One"]);
            Assert.IsTrue(result.Values.ContainsKey("Two"));
            Assert.AreEqual(2, result.Values["Two"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallRedirectWithDynamicValuesAndNullActionName()
        {
            // Setup
            dynamic values = new
            {
                One = 1,
                Two = 2,
            };

            // Execute
            var result = Redirect(a_actionName: null, a_values: values);
        }

        [TestMethod]
        public void CallRedirectWithDynamicValuesAndControllerName()
        {
            // Setup
            dynamic values = new
            {
                One = 1,
                Two = 2,
            };

            // Execute
            var result = Redirect(a_actionName: "ActionName", a_controllerName: "Controller", a_values: values);

            // Assert
            Assert.AreEqual("Controller", result.ControllerName);
            Assert.AreEqual("ActionName", result.ActionName);

            Assert.IsNotNull(result.Values);
            Assert.IsTrue(result.Values.ContainsKey("One"));
            Assert.AreEqual(1, result.Values["One"]);
            Assert.IsTrue(result.Values.ContainsKey("Two"));
            Assert.AreEqual(2, result.Values["Two"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallRedirectWithDynamicValuesAndNullControllerName()
        {
            // Setup
            dynamic values = new
            {
                One = 1,
                Two = 2,
            };

            // Execute
            var result = Redirect(a_actionName: "ActionName", a_controllerName: null, a_values: values);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallRedirectWithDynamicValuesAndControllerNameAndNullActionName()
        {
            // Setup
            dynamic values = new
            {
                One = 1,
                Two = 2,
            };

            // Execute
            var result = Redirect(a_actionName: null, a_controllerName: "Controller", a_values: values);
        }
    }
}
