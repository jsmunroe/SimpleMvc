using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc;
using SimpleMvc.Contracts;
using SimpleMvc.Exceptions;
using SimpleMvc.Handlers;
using SimpleMvc.Results;
using SimpleMvc.Test.TestViews;

namespace SimpleMvc.Test.Handlers
{
    [TestClass]
    public class ViewHandlerTest
    {
        private TestViewCatalog _viewCatalog = null;
        private TestModelBinder _modelBinder = null;
        private TestViewTarget _viewTarget = null;

        private ViewHandler InitializeViewHandler()
        {
            _viewCatalog = new TestViewCatalog();
            _modelBinder = new TestModelBinder();
            _viewTarget = new TestViewTarget();

            var viewHandler = new ViewHandler();
            viewHandler.RegisterViewCatalog(_viewCatalog);
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
            viewHandler.RegisterViewCatalog(new TestViewCatalog());
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
            var viewHandler = InitializeViewHandler();
            _viewCatalog.RegisterView<TestView1>("Index");
            var model = new TestModel();

            // Execute
            viewHandler.Handle(new ViewResult("Index", model));

            // Assert
            Assert.IsTrue(_viewCatalog.ViewNames.Contains("Index"));
            Assert.IsTrue(_viewTarget.LastSetView is TestView1);
            Assert.AreSame(model, (_viewTarget.LastSetView as TestView1).DataModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HandleViewResultWithNull()
        {
            // Setup
            var viewHandler = InitializeViewHandler();

            // Execute
            viewHandler.Handle(a_result: null);
        }

        [TestMethod]
        [ExpectedException(typeof(ViewNotFoundException))]
        public void HandleViewResultWithNonRegisterView()
        {
            // Setup
            var viewHandler = InitializeViewHandler();

            // Execute
            viewHandler.Handle(new ViewResult("Index", new TestModel()));

            // Assert
            Assert.IsTrue(_viewCatalog.ViewNames.Contains("Index"));
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
            viewHandler.Bootstrap(a_container:null);
        }
    }
}
