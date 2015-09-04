using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleMvc.Contracts;
using SimpleMvc.Handlers;
using SimpleMvc.Results;

namespace SimpleMvc.Test.Handlers
{
    [TestClass]
    public class RedirectHandlerTest
    {
        private Mock<INavigator> _mockNavigator;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockNavigator = new Mock<INavigator>();
        }

        [TestMethod]
        public void ConstructRedirectHandler()
        {
            // Execute
            new RedirectHandler(_mockNavigator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructRedirectHandlerWithNullNavigator()
        {
            // Execute
            new RedirectHandler(a_navigator: null);
        }


        [TestMethod]
        public void HandleRedirectResult()
        {
            // Setup
            var mvc = new MvcEngine();
            var handler = new RedirectHandler(_mockNavigator.Object);
            var values = new RouteDictionary();
            var result = new RedirectResult
            {
                ControllerName = "Test",
                ActionName = "Index",
                Values = new RouteDictionary(),
            };

            // Execute
            handler.Handle(mvc, "AnotherTest", result);

            // Assert
            _mockNavigator.Verify(i => i.Navigate("Test", "Index", values), Times.Once);
        }


        [TestMethod]
        public void HandleRedirectResultWithNoControllerName()
        {
            // Setup
            var mvc = new MvcEngine();
            var handler = new RedirectHandler(_mockNavigator.Object);
            var values = new RouteDictionary();
            var result = new RedirectResult
            {
                ControllerName = null,
                ActionName = "Index",
                Values = new RouteDictionary(),
            };

            // Execute
            handler.Handle(mvc, "AnotherTest", result);

            // Assert
            _mockNavigator.Verify(i => i.Navigate("AnotherTest", "Index", values), Times.Once);
        }

        [TestMethod]
        public void HandleRedirectResultWithNullMvcEngine()
        {
            // Setup
            var handler = new RedirectHandler(_mockNavigator.Object);
            var values = new RouteDictionary();
            var result = new RedirectResult
            {
                ControllerName = "Test",
                ActionName = "Index",
                Values = new RouteDictionary(),
            };

            // Execute
            handler.Handle(a_mvc: null, a_controllerName: "AnotherTest", a_result: result);

            // Assert
            _mockNavigator.Verify(i => i.Navigate("Test", "Index", values), Times.Once);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HandleRedirectResultWithNullControllerName()
        {
            // Setup
            var mvc = new MvcEngine();
            var handler = new RedirectHandler(_mockNavigator.Object);
            var values = new RouteDictionary();
            var result = new RedirectResult
            {
                ControllerName = "Test",
                ActionName = "Index",
                Values = new RouteDictionary(),
            };

            // Execute
            handler.Handle(a_mvc: mvc, a_controllerName:null, a_result:result);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HandleRedirectResultWithNullResulte()
        {
            // Setup
            var mvc = new MvcEngine();
            var handler = new RedirectHandler(_mockNavigator.Object);
            var values = new RouteDictionary();
            var result = new RedirectResult
            {
                ControllerName = "Test",
                ActionName = "Index",
                Values = new RouteDictionary(),
            };

            // Execute
            handler.Handle(a_mvc: mvc, a_controllerName: "AnotherTest", a_result: null);
        }




    }
}
