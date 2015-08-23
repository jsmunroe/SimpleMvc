using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleMvc.Wpf.Test
{
    [TestClass]
    public class FrameViewTargetTest
    {
        [TestMethod]
        public void ConstructFrameViewTarget()
        {
            // Execute
            new FrameViewTarget("ViewTarget");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNull()
        {
            // Execute
            new FrameViewTarget(a_targetName:null);
        }

        [TestMethod]
        public void RegisterControl()
        {
            // Setup
            var target = new FrameViewTarget("ViewTarget");

            // Execute
            target.RegisterControl(new Frame());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterControlWithNull()
        {
            // Setup
            var target = new FrameViewTarget("ViewTarget");

            // Execute
            target.RegisterControl(a_control:null);
        }

        [TestMethod]
        public void SetView()
        {
            // Setup
            UiDispatcherFetcher.Current?.ToString();
            var target = new FrameViewTarget("ViewTarget");
            var frame = new Frame { Visibility = Visibility.Visible };
            FrameViewTarget.SetRegisterAs(frame, "ViewTarget");
            var page = new Page { Visibility = Visibility.Visible };

            // Execute
            target.SetView(page);
        }


    }
}
