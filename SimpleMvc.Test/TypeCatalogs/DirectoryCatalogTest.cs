using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMvc.Contracts;
using SimpleMvc.Test.TestViews;
using SimpleMvc.Test.TestViews.TestController;
using SimpleMvc.ViewCatalogs;

namespace SimpleMvc.Test.TypeCatalogs
{
    [TestClass]
    public class DirectoryCatalogTest
    {
        [TestMethod]
        public void ConstructDirectoryViewCatalog()
        {
            // Execute
            var catalog = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "Views");

            // Assert
            Assert.AreEqual("SimpleMvc.Test.Views", catalog.Namespace);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullContainer()
        {
            // Execute
            new DirectoryCatalog(a_container: null, a_assembly: Assembly.GetExecutingAssembly(), a_directory: "Views");
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullAssembly()
        {
            // Execute
            new DirectoryCatalog(a_assembly: null, a_directory: "Views");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullDirectory()
        {
            // Execute
            new DirectoryCatalog(a_assembly: Assembly.GetExecutingAssembly(), a_directory: null);
        }

        [TestMethod]
        public void Resolve()
        {
            // Setup
            var catalog = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "TestViews");

            // Execute
            var view = catalog.Resolve("TestView1");

            // Assert
            Assert.IsTrue(view is TestView1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResolveWithNullName()
        {
            // Setup
            var catalog = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "TestViews");

            // Assert
            var view = catalog.Resolve(a_catalogName:null);
        }

        [TestMethod]
        public void ResolveWithSubbranch()
        {
            // Setup
            var catalog = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "TestViews");

            // Execute
            var view = catalog.Resolve("TestController", "TestView1");

            // Assert
            Assert.IsTrue(view is TestView1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResolveWithNullSubbranch()
        {
            // Setup
            var catalog = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "TestViews");

            // Execute
            var view = catalog.Resolve(a_sub: null, a_typeName: "TestView1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResolveWithSubbranchWithNullName()
        {
            // Setup
            var catalog = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "TestViews");

            // Execute
            var view = catalog.Resolve(a_sub: "TestController", a_typeName: null);
        }


        [TestMethod]
        public void ToTypeName()
        {
            // Setup
            var catalog = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "TestViews", "View");

            // Execute
            var typeName = catalog.ToTypeName("Test");

            // Assert
            Assert.AreEqual("TestView", typeName);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToTypeNameWithNull()
        {
            // Setup
            var catalog = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "TestViews", "View");

            // Execute
            var typeName = catalog.ToTypeName(a_catalogName: null);
        }


        [TestMethod]
        public void ToCatalogName()
        {
            // Setup
            var catalog = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "TestViews", "View");

            // Execute
            var catalogName = catalog.ToCatalogName("TestView");

            // Assert
            Assert.AreEqual("Test", catalogName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToCatalogNameWithNull()
        {
            // Setup
            var catalog = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "TestViews", "View");

            // Execute
            var catalogName = catalog.ToCatalogName(a_typeName: null);
        }


        [TestMethod]
        public void ToCatalogNameWithIncorrectSuffix()
        {
            // Setup
            var catalog = new DirectoryCatalog(Assembly.GetExecutingAssembly(), "TestViews", "View");


            // Execute
            var catalogName = catalog.ToCatalogName("TestXiew");

            // Assert
            Assert.AreEqual("TestXiew", catalogName);
        }


    }
}
