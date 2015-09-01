using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc;
using SimpleMvc.Test.TestControllers;
using SimpleMvc.Test.TestViews;
using SimpleMvc.Test.TestViews.TestController;

namespace SimpleMvc.Test
{
    [TestClass]
    public class NavigationCoreTest
    {
        private Container _container;
        private MvcEngine _mvcEngine;

        [TestInitialize()]
        public void Initialize()
        {
            _container = new Container();
            _mvcEngine = new MvcEngine(_container)
                .RegisterControllerCatalog("TestControllers", "Controller")
                .RegisterViewCatalog("TestViews")
                .RegisterModelBinder(new TestModelBinder());
        }

        [TestMethod]
        public void NavigateToAction()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate<TestController>(c => c.Index());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NavigateToActionWithNullExpression()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate<TestController>(a_actionExpression: null);
        }

        [TestMethod]
        public void NavigateToActionWithName()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate<TestController>("Index", new RouteDictionary());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NavigateToActionWithNullName()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate<TestController>(a_actionName: null, a_routeValues: new RouteDictionary());
        }

        [TestMethod]
        public void NavigateToActionWithNullRouteValues()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate<TestController>(a_actionName: "Index", a_routeValues: null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.NavigationException))]
        public void NavigateToNotExistingActionWithName()
       { 
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate<TestController>("Bindex", new RouteDictionary());
        }

        [TestMethod]
        public void NavigateToActionWithArguments()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            var routeDictionary = new RouteDictionary
            {
                { "id", 12 }
            };
            navigationCore.Navigate<TestController>("User", routeDictionary);
        }

        [TestMethod]
        public void NavigateToActionWithArgumentsOverload2()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            var routeDictionary = new RouteDictionary
            {
                { "username", "jsmunroe" },
                { "password", "password" }
            };
            navigationCore.Navigate<TestController>("User", routeDictionary);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.NavigationException))]
        public void NavigateToActionWithTooFewArguments()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            var routeDictionary = new RouteDictionary
            {
                { "username", "jsmunroe" },
            };
            navigationCore.Navigate<TestController>("User", routeDictionary);
        }


        [TestMethod]
        public void NavigateWithControllerName()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate("Test", "Index", new RouteDictionary());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NavigateWithNullControllerName()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate(a_controllerName:null, a_actionName:"Index", a_routeValues:new RouteDictionary());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NavigateWithControllerAndNullActionName()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate(a_controllerName: "Test", a_actionName: null, a_routeValues: new RouteDictionary());
        }


        [TestMethod]
        public void NavigateWithControllerAndNullRouteValues()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate(a_controllerName: "Test", a_actionName: "Index", a_routeValues: null);
        }




        [TestMethod]
        public void NavigateToActionWithNameAndDynamicRouteValues()
        {
            // Setup
            dynamic routeValues = new {};
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate<TestController>("Index", routeValues);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NavigateToActionWithNullNameAndDynamicRouteValues()
        {
            // Setup
            dynamic routeValues = new { };
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate<TestController>(a_actionName: null, a_routeValues: routeValues);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.NavigationException))]
        public void NavigateToNotExistingActionWithNameAndDynamicRouteValues()
        {
            // Setup
            dynamic routeValues = new { };
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate<TestController>("Bindex", routeValues);
        }

        [TestMethod]
        public void NavigateToActionWithArgumentsAndDynamicRouteValues()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            dynamic routeValues = new
            {
                id = 12
            };
            navigationCore.Navigate<TestController>("User", routeValues);
        }

        [TestMethod]
        public void NavigateToActionWithArgumentsOverload2AndDynamicRouteValues()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            dynamic routeValues = new
            {
                username = "jsmunroe",
                password = "password"
            };
            navigationCore.Navigate<TestController>("User", routeValues);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.NavigationException))]
        public void NavigateToActionWithTooFewArgumentsAndDynamicRouteValues()
        {
            // Setup
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            dynamic routeValues = new
            {
                username = "jsmunroe",
            };

            navigationCore.Navigate<TestController>("User", routeValues);
        }


        [TestMethod]
        public void NavigateWithControllerNameAndDynamicRouteValues()
        {
            // Setup
            dynamic routeValues = new { };
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate("Test", "Index", routeValues);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NavigateWithNullControllerNameAndDynamicRouteValues()
        {
            // Setup
            dynamic routeValues = new { };
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate(a_controllerName: null, a_actionName: "Index", a_routeValues: routeValues);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NavigateWithControllerAndNullActionNameAndDynamicRouteValues()
        {
            // Setup
            dynamic routeValues = new { };
            var navigationCore = new NavigationCore(_mvcEngine);

            // Execute
            navigationCore.Navigate(a_controllerName: "Test", a_actionName: null, a_routeValues: routeValues);
        }

    }
}