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
                .RegisterControllerCatalog("TestControllers");
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
        public void RegisterControllerCatalogWithNull()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            mvc.RegisterControllerCatalog(a_catalog: null);
        }


        [TestMethod]
        public void RegisterControllerCatalogWithAssembly()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            var result = mvc.RegisterControllerCatalog("Controllers", "", typeof(object));

            // Assert
            Assert.AreSame(mvc, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterControllerCatalogWithAssemblyWithNullDirectory()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            mvc.RegisterControllerCatalog(a_directory: null, a_suffix: "", a_baseType: typeof(object));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterControllerCatalogWithAssemblyWithNullSuffix()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            mvc.RegisterControllerCatalog( a_directory: "Controllers", a_suffix: null, a_baseType: typeof(object));
        }


        [TestMethod]
        public void RegisterViewCatalog()
        {
            // Setup
            var mvc = new MvcEngine();
            var viewCatalog = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "Views", "", typeof (object));

            // Execute
            var result = mvc.RegisterViewCatalog(viewCatalog);

            // Assert
            Assert.AreSame(mvc, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterViewCatalogWithNull()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            mvc.RegisterViewCatalog(a_catalog: null);
        }


        [TestMethod]
        public void RegisterViewCatalogWithAssembly()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            var result = mvc.RegisterViewCatalog("Views", "", typeof(object));

            // Assert
            Assert.AreSame(mvc, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterViewCatalogWithAssemblyWithNullDirectory()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            mvc.RegisterViewCatalog(a_directory: null, a_suffix: "", a_baseType: typeof(object));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterViewCatalogWithAssemblyWithNullSuffix()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            mvc.RegisterViewCatalog(a_directory: "Views", a_suffix: null, a_baseType: typeof(object));
        }


        [TestMethod]
        public void RegisterViewTarget()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            var result = mvc.RegisterViewTarget(new TestViewTarget());

            // Assert
            Assert.AreSame(mvc, result);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterViewTargetWithNull()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            var result = mvc.RegisterViewTarget(a_viewTarget: null);
        }

        [TestMethod]
        public void RegisterModelBinder()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            var result = mvc.RegisterModelBinder(new TestModelBinder());

            // Assert
            Assert.AreSame(mvc, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterModelBinderWithNull()
        {
            // Setup
            var mvc = new MvcEngine();

            // Execute
            var result = mvc.RegisterModelBinder(a_modelBinder: null);
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
            Assert.IsNotNull(controller.Mvc);
        }

        [TestMethod]
        public void ResolveControllerWithName()
        {
            // Execute
            var controller = _mvc.ResolveController("TestController");

            // Execute
            Assert.IsNotNull(controller);
            Assert.IsInstanceOfType(controller, typeof (TestController));
            Assert.IsNotNull((controller as TestController).Mvc);
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
                .RegisterControllerCatalog("TestControllers");

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
