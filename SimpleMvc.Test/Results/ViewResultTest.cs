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
            var viewResult = new ViewResult("Index", "MyModel");

            // Assert
            Assert.AreSame("Index", viewResult.ViewName);
            Assert.AreSame("MyModel", viewResult.Model);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullName()
        {
            // Execute
            var viewResult = new ViewResult(a_viewName: null, a_model: 32);
        }

        [TestMethod]
        public void ConstructWithNullModel()
        {
            // Execute
            var viewResult = new ViewResult(a_viewName: "UserDetails", a_model: null);

            // Assert
            Assert.AreSame("UserDetails", viewResult.ViewName);
            Assert.IsNull(viewResult.Model);
        }
    }
}
