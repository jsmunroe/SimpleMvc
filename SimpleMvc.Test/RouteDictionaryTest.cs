using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleMvc.Test
{
    [TestClass]
    public class RouteDictionaryTest
    {
        [TestMethod]
        public void ConstructRouteDictionary()
        {
            new RouteDictionary();
        }


        [TestMethod]
        public void ConstructWithDynamicObject()
        {
            // Setup
            dynamic values = new
            {
                Key = "key",
                Value = "value",
            };

            // Execute
            var dict = new RouteDictionary(values);

            // Assert
            Assert.IsTrue(dict.ContainsKey("Key"));
            Assert.AreEqual("key", dict["Key"]);

            Assert.IsTrue(dict.ContainsKey("Value"));
            Assert.AreEqual("value", dict["Value"]);
        }


        [TestMethod]
        public void ConstructWithNullDynamicObject()
        {
            // Execute
            new RouteDictionary(a_values: null);
        }

    }
}
