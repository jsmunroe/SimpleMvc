using CommunityToolkit.Mvvm.ComponentModel;
using SimpleMvc.Wpf.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpleMvc.Wpf.Helpers
{
    public partial class FocusInfo : ObservableViewModelBase
    {
        public static readonly DependencyProperty IsFocusInfoScopeProperty =
            DependencyProperty.RegisterAttached(
                "IsFocusInfoScope",
                typeof(bool),
                typeof(FocusInfo),
                new FrameworkPropertyMetadata(
                    defaultValue: false
                )
            );

        public static bool GetIsFocusInfoScope(UIElement target) => (bool)target.GetValue(IsFocusInfoScopeProperty);

        public static void SetIsFocusInfoScope(UIElement target, bool value) => target.SetValue(IsFocusInfoScopeProperty, value);

        public static readonly DependencyProperty FocusProperty =
            DependencyProperty.RegisterAttached(
                "Focus",
                typeof(FocusInfo),
                typeof(FocusInfo),
                new FrameworkPropertyMetadata(
                    defaultValue: null,
                    new PropertyChangedCallback(OnFocusPropertyChanged)
                )
            );

        public static FocusInfo GetFocus(UIElement target) => (FocusInfo)target.GetValue(FocusProperty);

        public static void SetFocus(UIElement target, FocusInfo focus) => target.SetValue(FocusProperty, focus);

        public static DependencyProperty FocussedElementProperty =
            DependencyProperty.RegisterAttached(
                "FocussedElement",
                typeof(object),
                typeof(FocusInfo),
                new FrameworkPropertyMetadata(
                    defaultValue: null
                )
            );

        public static object GetFocussedElement(UIElement target) => target.GetValue(FocussedElementProperty);

        public static void SetFocussedElement(UIElement target, object data) => target.SetValue(FocussedElementProperty, data);



        public static readonly DependencyProperty IsMouseOverProperty =
            DependencyProperty.RegisterAttached(
                "IsMouseOver",
                typeof(bool),
                typeof(FocusInfo),
                new FrameworkPropertyMetadata(
                    defaultValue: false
                )
            );

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
                "IsFocused",
                typeof(bool),
                typeof(FocusInfo),
                new FrameworkPropertyMetadata(
                    defaultValue: false
                )
            );

        public List<FrameworkElement> Elements { get; set; } = [];

        [ObservableProperty]
        private bool _isMouseOver = false;

        [ObservableProperty]
        private bool _isFocused = false;

        partial void OnIsMouseOverChanged(bool value)
        {
            foreach (var element in Elements)
                element.SetValue(IsMouseOverProperty, value);
        }

        partial void OnIsFocusedChanged(bool value)
        {
            foreach (var element in Elements)
                element.SetValue(IsFocusedProperty, value);
        }

        private static void OnFocusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement element)
                return;

            if (e.OldValue is FocusInfo oldValue)
            {
                element.GotFocus -= oldValue.OnGotFocus;
                element.LostFocus -= oldValue.OnLostFocus;
                element.MouseEnter -= oldValue.OnMouseEnter;
                element.MouseLeave -= oldValue.OnMouseLeave;

                oldValue.Elements.Remove(element);
            }

            if (e.NewValue is FocusInfo newValue)
            {
                element.GotFocus += newValue.OnGotFocus;
                element.LostFocus += newValue.OnLostFocus;
                element.MouseEnter += newValue.OnMouseEnter;
                element.MouseLeave += newValue.OnMouseLeave;

                newValue.IsMouseOver = element.IsMouseOver;
                newValue.IsFocused = element.IsFocused;
                newValue.Elements.Add(element);
            }
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (!Elements.Contains(sender))
                return;

            IsFocused = true;

            if (sender is FrameworkElement element && element.FindParentWithAttachedPropertyTrue(IsFocusInfoScopeProperty) is FrameworkElement scopeElement)
            {
                SetFocussedElement(element, element.DataContext);
            }
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            var focusedElement = FocusManager.GetFocusedElement(Application.Current.MainWindow);

            if (!Elements.Contains(sender))
                return;

            IsFocused = false;
        }

        private void OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!Elements.Contains(sender))
                return;

            IsMouseOver = true;
        }

        private void OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!Elements.Contains(sender))
                return;

            IsMouseOver = false;
        }
    }
}
