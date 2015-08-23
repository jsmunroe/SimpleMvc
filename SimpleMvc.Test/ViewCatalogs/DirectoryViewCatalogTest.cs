using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMvc.Contracts;
using SimpleMvc.Test.TestViews;
using SimpleMvc.ViewCatalogs;

namespace SimpleMvc.Test.ViewCatalogs
{
    [TestClass]
    public class DirectoryViewCatalogTest
    {
        [TestMethod]
        public void ConstructDirectoryViewCatalog()
        {
            // Execute
            var catalog = new DirectoryViewCatalog(Assembly.GetExecutingAssembly(), "Views");

            // Assert
            Assert.AreEqual("SimpleMvc.Test.Views", catalog.Namespace);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullContainer()
        {
            // Execute
            new DirectoryViewCatalog(a_container: null, a_assembly: Assembly.GetExecutingAssembly(), a_directory: "Views");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullAssembly()
        {
            // Execute
            new DirectoryViewCatalog(a_assembly: null, a_directory: "Views");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullDirectory()
        {
            // Execute
            new DirectoryViewCatalog(a_assembly: Assembly.GetExecutingAssembly(), a_directory: null);
        }

        [TestMethod]
        public void GetView()
        {
            // Setup
            var catalog = new DirectoryViewCatalog(Assembly.GetExecutingAssembly(), "TestViews");

            // Execute
            var view = catalog.GetView("TestView1");

            // Assert
            Assert.IsTrue(view is TestView1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetViewWithNullName()
        {
            // Setup
            var catalog = new DirectoryViewCatalog(Assembly.GetExecutingAssembly(), "TestViews");

            // Assert
            var view = catalog.GetView(a_viewName:null);
        }

        
    }
}
