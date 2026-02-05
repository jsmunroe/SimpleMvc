using CommunityToolkit.Mvvm.ComponentModel;
using SimpleMvc.Contracts;
using System;
using System.Collections.Generic;

namespace SimpleMvc.Wpf
{
    public class ObservableViewModelBase : ObservableObject, IViewModel
    {
        protected INavigator Navigator { get; private set; }
        protected string ControllerName { get; private set; }
        protected SimpleIoc.Contracts.IContainer Container { get; private set; }

        protected virtual void Load()
        {
            // Override in derived classes to initialize
        }

        public virtual void Cleanup()
        {
            // Override in derived classes to perform cleanup tasks
        }

        protected TViewModel Afix<TViewModel>(TViewModel child)
            where TViewModel : IViewModel
        {
            ArgumentNullException.ThrowIfNull(child);

            child.Navigator = Navigator;
            child.ControllerName = ControllerName;
            child.Container = Container;

            return child;
        }

        protected IEnumerable<TViewModel> Afix<TViewModel>(IEnumerable<TViewModel> children)
            where TViewModel : IViewModel
        {
            ArgumentNullException.ThrowIfNull(children);

            foreach (var child in children)
            {
                child.Navigator = Navigator;
                child.ControllerName = ControllerName;
                child.Container = Container;
            }

            return children;
        }

        #region IViewModel members

        INavigator IViewModel.Navigator { get => Navigator; set => Navigator = value; }
        string IViewModel.ControllerName { get => ControllerName; set => ControllerName = value; }
        SimpleIoc.Contracts.IContainer IViewModel.Container { get => Container; set => Container = value; }

        void IViewModel.Cleanup()
        {
            Cleanup();
        }

        void IViewModel.Load()
        {
            Load();
        }

        #endregion
    }
}
