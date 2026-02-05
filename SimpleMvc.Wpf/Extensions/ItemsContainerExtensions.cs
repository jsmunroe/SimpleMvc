using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleMvc.Wpf.Extensions
{
    public static class ItemsControlExtensions
    {
        public static List<TElement> GetListElements<TElement>(this ItemsControl itemsControl)
            where TElement : FrameworkElement
        {
            var items = itemsControl.Items.OfType<object>();

            var elements = new List<TElement>();
            foreach (var item in items)
            {
                var element = GetElement<TElement>(itemsControl, item);

                if (element is null)
                    continue;

                elements.Add(element);
            }

            return elements;
        }

        public static List<FrameworkElement> GetListElements(this ItemsControl itemsControl)
        {
            return GetListElements<FrameworkElement>(itemsControl);
        }

        public static TElement GetElement<TElement>(this ItemsControl itemsControl, object item)
            where TElement : FrameworkElement
        {
            itemsControl.UpdateLayout();
            var container = itemsControl.ItemContainerGenerator.ContainerFromItem(item);

            if (container is null)
                return default;

            if (VisualTreeHelper.GetChild(container, 0) is not TElement element)
                return default;

            return element;

        }

        public static FrameworkElement GetElement(this ItemsControl itemsControl, object item)
        {
            return GetElement<FrameworkElement>(itemsControl, item);
        }
    }
}
