using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc;
using SimpleMvc.Contracts;
using SimpleMvc.Exceptions;
using SimpleMvc.Handlers;
using SimpleMvc.Results;
using SimpleMvc.Test.TestControllers;
using SimpleMvc.Test.TestViews;
using SimpleMvc.Test.TestViews.TestController;

namespace SimpleMvc.Test.Handlers
{
    [TestClass]
    public class ViewHandlerTest
    {
        private TestTypeCatalog _typeCatalog = null;
        private TestModelBinder _modelBinder = null;
        private TestViewTarget _viewTarget = null;

        private ViewHandler InitializeViewHandler()
        {
            _typeCatalog = new TestTypeCatalog();
            _modelBinder = new TestModelBinder();
            _viewTarget = new TestViewTarget();

            var viewHandler = new ViewHandler();
            viewHandler.RegisterViewCatalog(_typeCatalog);
            viewHandler.RegisterModelBinder(_modelBinder);
            viewHandler.RegisterViewTarget(_viewTarget);

            return viewHandler;
        }

        [TestMethod]
        public void ConstructViewHandler()
        {
            // Execute
            new ViewHandler();
        }

        [TestMethod]
        public void RegisterViewCatalog()
        {
            // Setup
            var viewHandler = new ViewHandler();

            // Execute
            viewHandler.RegisterViewCatalog(new TestTypeCatalog());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterViewCatalogWithNull()
        {
            // Setup
            var viewHandler = new ViewHandler();

            // Execute
            viewHandler.RegisterViewCatalog(a_viewCatalog: null);
        }

        [TestMethod]
        public void RegisterViewTarget()
        {
            // Setup
            var viewHandler = new ViewHandler();

            // Execute
            viewHandler.RegisterViewTarget(new TestViewTarget());
        }

        [TestMethod]
        public void RegisterMultipleViewTargets()
        {
            // Setup
            var viewHandler = new ViewHandler();

            // Execute
            viewHandler.RegisterViewTarget(new TestViewTarget());
            viewHandler.RegisterViewTarget(new TestViewTarget());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterViewTargetWithNull()
        {
            // Setup
            var viewHandler = new ViewHandler();

            // Execute
            viewHandler.RegisterViewTarget(a_viewTarget: null);
        }

        [TestMethod]
        public void RegisterModelBinder()
        {
            // Setup
            var viewHandler = new ViewHandler();

            // Execute
            viewHandler.RegisterModelBinder(new TestModelBinder());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterModelBinderWithNull()
        {
            // Setup
            var viewHandler = new ViewHandler();

            // Execute
            viewHandler.RegisterModelBinder(a_modelBinder: null);
        }


        [TestMethod]
        public void HandleViewResult()
        {
            // Setup
            var mvc = new MvcEngine();
            var controller = new TestController();
            var viewHandler = InitializeViewHandler();
            _typeCatalog.RegisterType<TestView1>("Index");
            var model = new TestModel();

            // Execute
            viewHandler.Handle(mvc, controller.GetType().Name, new ViewResult { ViewName = "Index", Model = model });

            // Assert
            Assert.IsTrue(_typeCatalog.TypeNames.Contains("Index"));
            Assert.IsTrue(_viewTarget.LastSetView is TestView1);
            Assert.AreSame(model, (_viewTarget.LastSetView as TestView1).DataModel);
        }

        [TestMethod]
        public void HandleViewResultAgain()
        {
            // Setup
            var mvc = new MvcEngine();
            var controller = new TestController();
            var viewHandler = InitializeViewHandler();
            _typeCatalog.RegisterType<TestView1>("Index");
            _typeCatalog.RegisterType<TestView2>("About");

            var model = new TestMvcModel();
            viewHandler.Handle(mvc, controller.GetType().Name, new ViewResult { ViewName = "Index", Model = model });

            // Execute
            viewHandler.Handle(mvc, controller.GetType().Name, new ViewResult { ViewName = "About" });

            // Assert
            Assert.IsTrue(model.CleanupCalled);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HandleViewResultWithNullMvcEngine()
        {
            // Setup
            var viewHandler = InitializeViewHandler();
            _typeCatalog.RegisterType<TestView1>("Index");
            var model = new TestModel();

            // Execute
            viewHandler.Handle(a_mvc: null, a_controllerName: "TestController", a_result: new ViewResult { ViewName = "Index", Model = model });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HandleViewResultWithNullController()
        {
            // Setup
            var mvc = new MvcEngine();
            var viewHandler = InitializeViewHandler();
            _typeCatalog.RegisterType<TestView1>("Index");
            var model = new TestModel();

            // Execute
            viewHandler.Handle(mvc, a_controllerName: null, a_result: new ViewResult { ViewName = "Index", Model = model });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HandleViewResultWithNullResult()
        {
            // Setup
            var mvc = new MvcEngine();
            var viewHandler = InitializeViewHandler();

            // Execute
            viewHandler.Handle(mvc, a_controllerName: "TestController", a_result: null);
        }

        [TestMethod]
        [ExpectedException(typeof(TypeNotFoundException))]
        public void HandleViewResultWithNonRegisterView()
        {
            // Setup
            var mvc = new MvcEngine();
            var viewHandler = InitializeViewHandler();
            var model = new TestModel();

            // Execute
            viewHandler.Handle(mvc, "TestController", new ViewResult { ViewName = "Index", Model = model });

            // Assert
            Assert.IsTrue(_typeCatalog.TypeNames.Contains("Index"));
        }

        [TestMethod]
        public void Bootstrap()
        {
            // Setup
            var container = new Container();
            var viewHandler = InitializeViewHandler();

            // Execute
            viewHandler.Bootstrap(container);

            // Assert
            Assert.IsTrue(viewHandler.IsBootstrapped);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BootstrapWithNull()
        {
            // Setup
            var viewHandler = InitializeViewHandler();

            // Execute
            viewHandler.Bootstrap(a_container: null);
        }


    }
}
