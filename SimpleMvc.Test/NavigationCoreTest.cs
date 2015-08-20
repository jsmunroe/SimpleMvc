using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleMvc.Test
{
    [TestClass]
    public class NavigationCoreTest
    {
        [TestMethod]
        public void NavigateToAction()
        {
            // Setup
            var navigationCore = new NavigationCore();

            // Execute
            navigationCore.Navigate<FakeContrller>(c => c.Index()); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NavigateToActionWithNullExpression()
        {
            // Setup
            var navigationCore = new NavigationCore();

            // Execute
            navigationCore.Navigate<FakeContrller>(a_actionExpression: null);
        }

        [TestMethod]
        public void NavigateToActionWithName()
        {
            // Setup
            var navigationCore = new NavigationCore();

            // Execute
            navigationCore.Navigate<FakeContrller>("Index", new RouteDictionary());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NavigateToActionWithNullName()
        {
            // Setup
            var navigationCore = new NavigationCore();

            // Execute
            navigationCore.Navigate<FakeContrller>(a_actionName: null, a_routeValues: new RouteDictionary());
        }

        [TestMethod]
        public void NavigateToActionWithNullRouteValues()
        {
            // Setup
            var navigationCore = new NavigationCore();

            // Execute
            navigationCore.Navigate<FakeContrller>(a_actionName: "Index", a_routeValues: null);
        }

        [TestMethod]
        [ExpectedException(typeof(NavigationException))]
        public void NavigateToNotExistingActionWithName()
        {
            // Setup
            var navigationCore = new NavigationCore();

            // Execute
            navigationCore.Navigate<FakeContrller>("Bindex", new RouteDictionary());
        }
    }
}