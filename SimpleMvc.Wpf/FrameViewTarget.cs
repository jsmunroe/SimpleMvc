using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SimpleMvc.Wpf
{
    public class FrameViewTarget : ViewTarget<FrameworkElement>
    {
        public static readonly DependencyProperty RegisterAsProperty = DependencyProperty.RegisterAttached("RegisterAs", typeof(string), typeof(FrameViewTarget), new FrameworkPropertyMetadata(null, OnRegisterAsPropertyChanged));

        /// <summary>
        /// Set this target to the given view (<paramref name="a_view"/>).
        /// </summary>
        /// <param name="a_view">View object.</param>
        public override void SetView(FrameworkElement a_view)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the value of the RegisterAs attached dependency property.
        /// </summary>
        /// <param name="ob">Dependency object containing the value.</param>
        /// <returns>Attached dependency property value.</returns>
        public static string GetRegisterAs(DependencyObject ob)
        {
            if (ob == null)
                return default(string);

            var value = ob.GetValue(RegisterAsProperty);

            if (value is string == false)
                return default(string);

            return (string)ob.GetValue(RegisterAsProperty);
        }

        /// <summary>
        /// Set the value of the RegisterAs attached dependency property.
        /// </summary>
        /// <param name="ob">Dependency object containing the value.</param>
        /// <param name="value">Attached dependency property value.</param>
        public static void SetRegisterAs(DependencyObject ob, string value)
        {
            ob.SetValue(RegisterAsProperty, value);
        }

        /// <summary>
        /// When the RegisterAsProperty dependency property is changed.
        /// </summary>
        private static void OnRegisterAsPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var name = e.NewValue;
        }


    }
}
