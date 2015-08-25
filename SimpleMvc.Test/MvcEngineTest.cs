using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc;
using SimpleMvc.Contracts;
using SimpleIoc.Contracts;
using SimpleMvc.Exceptions;
using SimpleMvc.Results;
using SimpleMvc.Test.TestControllers;
using SimpleMvc.Test.TestViews;
using SimpleMvc.Test.TestViews.TestController;
using SimpleMvc.ViewCatalogs;

namespace SimpleMvc.Test
{
    [TestClass]
    public class MvcEngineTest
    {
        private Container _container;
        private MvcEngine _mvc = null;

        [TestInitialize()]
        public void Initialize()
        {
            _container = new Container();
            _mvc = new MvcEngine(_container)
                .RegisterControllerCatalog(Assembly.GetExecutingAssembly(), "TestControllers");
        }

        [TestMethod]
        public void ConstructMvcEngine()
        {
            // Exec
            new MvcEngine();
        }

        [TestMethod]
        public void RegisterControllerCatalog()
        {
            // Setup
            var mvc = new MvcEngine();
            var catalogController = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "Controllers");

            // Execute
            var result = mvc.RegisterControllerCatalog(catalogController);

            // Assert
            Assert.AreSame(mvc, result);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterControllerWithNull()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            mvc.RegisterControllerCatalog(a_catalog: null);
        }


        [TestMethod]
        public void RegisterControllerWithAssembly()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            var result = mvc.RegisterControllerCatalog(Assembly.GetExecutingAssembly(), "Controllers", "", typeof(object));

            // Assert
            Assert.AreSame(mvc, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterControllerWithNullAssembly()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            mvc.RegisterControllerCatalog(a_assembly: null, a_directory: "Controllers", a_suffix: "", a_baseType: typeof(object));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterControllerWithAssemblyWithNullDirectory()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            mvc.RegisterControllerCatalog(a_assembly: Assembly.GetExecutingAssembly(), a_directory: null, a_suffix: "", a_baseType: typeof(object));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterControllerWithAssemblyWithNullSuffix()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            mvc.RegisterControllerCatalog(a_assembly: Assembly.GetExecutingAssembly(), a_directory: "Controllers", a_suffix: null, a_baseType: typeof(object));
        }

        [TestMethod]
        public void RegisterViewHandler()
        {
            // Execute
            _mvc.RegisterHandler(new TestViewHandler());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterHandlerWithNull()
        {
            // Execute
            _mvc.RegisterHandler<ViewResult>(a_handler: null);
        }

        [TestMethod]
        public void RegisterBootstrappableViewHandler()
        {
            // Setup
            var viewHandler = new TestViewHandler();

            // Execute
            _mvc.RegisterHandler(viewHandler);

            // Assert
            Assert.IsTrue(viewHandler.IsBootstrapped);
        }

        [TestMethod]
        public void RegisterTwoHandlersWithSameResultType()
        {
            // Execute
            _mvc.RegisterHandler(new TestViewHandler());
            _mvc.RegisterHandler(new TestViewHandler());
        }

        [TestMethod]
        public void ResolveController()
        {
            // Execute
            var controller = _mvc.ResolveController<TestController>();

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void ResolveControllerWithName()
        {
            // Execute
            _mvc.ResolveController("TestController");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResolveControllerWithNullName()
        {
            // Execute
            _mvc.ResolveController(a_controllerName: null);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ResolveControllerWithNameWithoutRegisteredCatalog()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            mvc.ResolveController("TestController");
        }

        [TestMethod]
        [ExpectedException(typeof(TypeNotFoundException))]
        public void ResolveNonExistantController()
        {
            // Setup
            var mvc = new MvcEngine()
                .RegisterControllerCatalog(Assembly.GetExecutingAssembly(), "TestControllers");

            // Execute
            mvc.ResolveController("CatController");
        }


        [TestMethod]
        public void HandleResult()
        {
            // Setup
            var handler1 = new TestViewHandler();
            handler1.TypeCatalog.RegisterType<TestView1>("MillionDollars");
            _mvc.RegisterHandler(handler1);
            var handler2 = new TestViewHandler();
            handler2.TypeCatalog.RegisterType<TestView1>("MillionDollars");
            _mvc.RegisterHandler(handler2);

            // Execute
            var controller = new TestController();
            _mvc.HandleResult(controller, new ViewResult { ViewName = "MillionDollars", Model = 1000000.0m });

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
            // Setup
            var controller = new TestController();

            // Execute
            _mvc.HandleResult(controller, new ViewResult { ViewName = "Oops", Model = "This is a model." });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HandleResultWithNullResult()
        {
            // Setup
            var controller = new TestController();

            // Execute
            _mvc.HandleResult(a_controller: controller, a_result: null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HandleResultWithNullController()
        {
            // Execute
            _mvc.HandleResult(a_controller: null, a_result: new ViewResult { ViewName = "Oops", Model = "This is a model." });
        }

        [TestMethod]
        public void NavigateToView()
        {
            // Setup
            var handler = new TestViewHandler();
            _mvc.RegisterHandler(handler);
            handler.TypeCatalog.RegisterType<TestView1>("Index");

            // Execute
            _mvc.Navigator.Navigate<TestController>(c => c.Index());

            // Assert
            Assert.IsNotNull(handler.LastResolvedView);
        }

        [TestMethod]
        [ExpectedException(typeof(MvcHandlerException))]
        public void NavigateToViewWithoutViewHandler()
        {
            // Execute
            _mvc.Navigator.Navigate<TestController>(c => c.Index());
        }

    }
}
