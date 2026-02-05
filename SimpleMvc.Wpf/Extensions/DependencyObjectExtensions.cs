using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SimpleMvc.Wpf.Extensions
{
    public static class DependencyObjectExtensions
    {
        public static DependencyObject FindParentWithAttachedPropertyValue<TValue>(this DependencyObject child, DependencyProperty property, TValue value)
        {
            var current = child;

            while (current != null)
            {
                if (Equals(current.GetValue(property), value))
                {
                    return current;
                }

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }

        public static DependencyObject FindParentWithAttachedPropertyTrue(this DependencyObject child, DependencyProperty property)
        {
            return FindParentWithAttachedPropertyValue(child, property, true);
        }
    }
}
