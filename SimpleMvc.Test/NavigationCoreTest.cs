using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc;

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
            _mvcEngine = new MvcEngine(_container);
            _mvcEngine.RegisterHandler(new TestViewHandler());
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

    }
}