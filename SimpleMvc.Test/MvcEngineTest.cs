using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc;
using SimpleMvc.Contracts;
using SimpleIoc.Contracts;
using SimpleMvc.Exceptions;
using SimpleMvc.Results;

namespace SimpleMvc.Test
{
    [TestClass]
    public class MvcEngineTest
    {
        private Container _container;
        private MvcEngine _mvcEngine = null;

        [TestInitialize()]
        public void Initialize()
        {
            _container = new Container();
            _mvcEngine = new MvcEngine(_container);
        }

        [TestMethod]
        public void ConstructMvcEngine()
        {
            // Exec
            new MvcEngine();
        }

        [TestMethod]
        public void ResolveController()
        {
            // Execute
            var controller = _mvcEngine.ResolveController<TestController>();

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void RegisterViewHandler()
        {
            // Execute
            _mvcEngine.RegisterHandler(new TestViewHandler());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterHandlerWithNull()
        {
            // Execute
            _mvcEngine.RegisterHandler<ViewResult>(a_handler: null);
        }

        [TestMethod]
        public void RegisterBootstrappableViewHandler()
        {
            // Setup
            var viewHandler = new TestViewHandler();

            // Execute
            _mvcEngine.RegisterHandler(viewHandler);

            // Assert
            Assert.IsTrue(viewHandler.IsBootstrapped);
        }

        [TestMethod]
        public void RegisterTwoHandlersWithSameResultType()
        {
            // Execute
            _mvcEngine.RegisterHandler(new TestViewHandler());
            _mvcEngine.RegisterHandler(new TestViewHandler());
        }

        [TestMethod]
        public void HandleResult()
        {
            // Setup
            var handler1 = new TestViewHandler();
            handler1.ViewCatalog.RegisterView<TestView1>("MillionDollars");
            _mvcEngine.RegisterHandler(handler1);
            var handler2 = new TestViewHandler();
            handler2.ViewCatalog.RegisterView<TestView1>("MillionDollars");
            _mvcEngine.RegisterHandler(handler2);

            // Execute
            _mvcEngine.HandleResult(new ViewResult("MillionDollars", 1000000.0m));

            // Assert
            Assert.IsNotNull(handler1.LastResolvedView);
            Assert.AreEqual("MillionDollars", handler1.LastResolvedView.Key);
            Assert.AreEqual(1000000.0m, handler1.LastResolvedView.Value);

            Assert.IsNotNull(handler2.LastResolvedView);
            Assert.AreEqual("MillionDollars", handler2.LastResolvedView.Key);
            Assert.AreEqual(1000000.0m, handler2.LastResolvedView.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(MvcHandlerException))]
        public void HandleResultWithoutHandler()
        {
            // Execute
            _mvcEngine.HandleResult(new ViewResult("Oops", "This is a model."));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HandleResultWithNull()
        {
            // Execute
            _mvcEngine.HandleResult(a_result: null);
        }

        [TestMethod]
        public void NavigateToView()
        {
            // Setup
            var handler = new TestViewHandler();
            _mvcEngine.RegisterHandler(handler);
            handler.ViewCatalog.RegisterView<TestView1>("Index");

            // Execute
            _mvcEngine.Navigator.Navigate<TestController>(c => c.Index());

            // Assert
            Assert.IsNotNull(handler.LastResolvedView);
        }

        [TestMethod]
        [ExpectedException(typeof(MvcHandlerException))]
        public void NavigateToViewWithoutViewHandler()
        {
            // Execute
            _mvcEngine.Navigator.Navigate<TestController>(c => c.Index());
        }

    }
}
