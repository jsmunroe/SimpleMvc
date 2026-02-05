using SimpleMvc.Wpf.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SimpleMvc.Wpf.Helpers
{
    public class ScrollSync
    {
        #region Attached Properties
        public static readonly DependencyProperty SyncGroupProperty =
            DependencyProperty.RegisterAttached(
            "SyncGroup",
            typeof(string),
            typeof(ScrollSync),
            new PropertyMetadata(null, OnSyncGroupPropertyChanged));

        public static string GetSyncGroup(DependencyObject obj)
        {
            return (string)obj.GetValue(SyncGroupProperty);
        }

        public static void SetSyncGroup(DependencyObject obj, string value)
        {
            obj.SetValue(SyncGroupProperty, value);
        }

        public static readonly DependencyProperty IsSharedScrollScopeProperty =
            DependencyProperty.RegisterAttached(
            "IsSharedScrollScope",
            typeof(bool),
            typeof(ScrollSync),
            new PropertyMetadata(false, OnIsSharedScrollScopePropertyChanged));

        public static bool GetIsSharedScrollScope(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSharedScrollScopeProperty);
        }

        public static void SetIsSharedScrollScope(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSharedScrollScopeProperty, value);
        }

        private static readonly DependencyProperty ScrollSyncProperty =
            DependencyProperty.RegisterAttached(
            "ScrollSync",
            typeof(ScrollSync),
            typeof(ScrollSync),
            new PropertyMetadata(null));

        private static void OnSyncGroupPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ScrollViewer scrollViewer)
                return;

            var scopeElement = scrollViewer.FindParentWithAttachedPropertyTrue(IsSharedScrollScopeProperty);

            if (scopeElement is null)
            {
                Debug.WriteLine("ScrollSync could not find a scope element. Scroll synchronization will be disabled!");
                return;
            }

            var scrollSync = (ScrollSync)scopeElement.GetValue(ScrollSyncProperty);

            if (e.OldValue is string oldGroupName)
            {
                scrollSync.RemoveScrollViewer(scrollViewer, oldGroupName);
            }

            if (e.NewValue is string newGroupName)
            {
                scrollSync.AddScrollViewer(scrollViewer, newGroupName);
            }

        }

        private static void OnIsSharedScrollScopePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollSync = new ScrollSync();

            d.SetValue(ScrollSyncProperty, scrollSync);
        } 
        #endregion

        public Dictionary<string, List<ScrollViewer>> SyncGroups { get; set; } = [];

        public bool IsSynching { get; set; } = false;

        private void AddScrollViewer(ScrollViewer scrollViewer, string groupName)
        {
            if (SyncGroups.TryGetValue(groupName, out var group))
            {
                if (!group.Contains(scrollViewer))
                    group.Add(scrollViewer);
            }
            else
            {
                SyncGroups[groupName] = [scrollViewer];
            }

                scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            scrollViewer.Unloaded += OnScrollViewerUnloaded;
        }

        private bool RemoveScrollViewer(ScrollViewer scrollViewer, string groupName)
        {
            if (SyncGroups.TryGetValue(groupName, out var group))
                return group.Remove(scrollViewer);

            return false;
        }

        private void OnScrollViewerUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not ScrollViewer scrollViewer)
                return;

            var groupName = GetSyncGroup(scrollViewer);

            if (groupName is null)
                return;

            RemoveScrollViewer(scrollViewer, groupName);
        }

        private void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (IsSynching)
                return;

            IsSynching = true;

            try
            {
                if (sender is not ScrollViewer scrollViewer)
                    return;

                var groupName = GetSyncGroup(scrollViewer);

                if (!SyncGroups.TryGetValue(groupName, out var group))
                    return;

                foreach (var element in group)
                {
                    if (ReferenceEquals(element, scrollViewer))
                        continue;

                    element.ScrollToVerticalOffset(scrollViewer.VerticalOffset);
                    element.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset);
                }
            }
            finally
            {
                IsSynching = false;
            }
        }

    }
}
