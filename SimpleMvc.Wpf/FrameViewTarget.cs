using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SimpleMvc.Wpf
{
    public class FrameViewTarget : ControlViewTarget<Frame>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_targetName">Target name.</param>
        public FrameViewTarget(string a_targetName) 
            : base(a_targetName)
        {

        }

        /// <summary>
        /// Set this target to the given view (<paramref name="a_view"/>).
        /// </summary>
        /// <param name="a_view">View object.</param>
        public override void SetView(FrameworkElement a_view)
        {
            _control?.Dispatcher.Invoke(() =>
            {
                _control.Navigate(a_view);
            });
        }
    }
}
