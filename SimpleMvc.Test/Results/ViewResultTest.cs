using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMvc.Results;

namespace SimpleMvc.Test.Results
{
    [TestClass]
    public class ViewResultTest
    {
        [TestMethod]
        public void Construct()
        {
            // Execute
            var viewResult = new ViewResult
            {
                ViewName = "Index",
                Model = "MyModel"
            };

            Assert.AreEqual("Index", viewResult.ViewName);
            Assert.AreEqual("MyModel", viewResult.Model);
        }


        [TestMethod]
        public void SetProperties()
        {
            // Setup
            var viewResult = new ViewResult();

            viewResult.ViewName = "Index";
            viewResult.Model = "MyModel";

            // Assert
            Assert.AreEqual("Index", viewResult.ViewName);
            Assert.AreEqual("MyModel", viewResult.Model);
        }
    }
}
