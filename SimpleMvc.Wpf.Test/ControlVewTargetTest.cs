using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleMvc.Wpf.Test
{
    [TestClass]
    public class ControlViewTargetTest
    {
        [TestMethod]
        public void ConstructControlViewTarget()
        {
            // Execute
            new ControlViewTarget<ContentControl>("ViewTarget");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNull()
        {
            // Execute
            new ControlViewTarget<ContentControl>(a_targetName:null);
        }

        [TestMethod]
        public void RegisterControl()
        {
            // Setup
            var target = new ControlViewTarget<ContentControl>("ViewTarget");

            // Execute
            target.RegisterControl(new ContentControl());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterControlWithNull()
        {
            // Setup
            var target = new ControlViewTarget<ContentControl>("ViewTarget");

            // Execute
            target.RegisterControl(a_control:null);
        }

        [TestMethod]
        public void SetView()
        {
            // Setup
            UiDispatcherFetcher.Current?.ToString();
            var target = new ControlViewTarget<ContentControl>("ViewTarget");
            var frame = new Control { Visibility = Visibility.Visible };
            ControlViewTarget<ContentControl>.SetRegisterAs(frame, "ViewTarget");
            var page = new Page { Visibility = Visibility.Visible };

            // Execute
            target.SetView(page);
        }


    }
}
