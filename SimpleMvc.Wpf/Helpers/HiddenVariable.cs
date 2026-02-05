using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleMvc.Wpf.Helpers
{
    public class HiddenVariable : FrameworkElement
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(DependencyObject), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnValueChanged)));

        public event EventHandler<object> ValueChanged;

        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var hiddenVariable = d as HiddenVariable;

            if (hiddenVariable is null)
                return;

            hiddenVariable.OnValueChanged(e.NewValue);
        }

        private void OnValueChanged(object value)
        {
            ValueChanged?.Invoke(this, value);
        }
    }
}
