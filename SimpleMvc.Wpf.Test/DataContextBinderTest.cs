using System;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMvc.Contracts;

namespace SimpleMvc.Wpf.Test
{
    [TestClass]
    public class DataContextBinderTest
    {
        [TestMethod]
        public void ConstructDataContextBinder()
        {
            // Execute
            new DataContextBinder();
        }

        [TestMethod]
        public void BindModelToView()
        {
            // Setup
            var binder = new DataContextBinder();
            var view = new Page();
            var model = "This is a model.";

            // Execute
            binder.Bind(view, model);

            // Assert
            Assert.IsNotNull(view.DataContext);
            Assert.AreEqual(model, view.DataContext);
            Assert.AreSame(model, view.DataContext);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BindWithNullView()
        {
            // Setup
            var binder = new DataContextBinder();
            var model = "This is a model.";

            // Execute
            binder.Bind(a_view: null, a_model: model);
        }


        [TestMethod]
        public void BindWithNullModel()
        {
            // Setup
            var binder = new DataContextBinder();
            var view = new Page();

            // Execute
            binder.Bind(a_view: view, a_model: null);

            // Assert
            Assert.IsNull(view.DataContext);
        }
    }
}
