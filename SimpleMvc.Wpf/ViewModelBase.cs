using SimpleIoc.Contracts;
using SimpleMvc.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Wpf
{
    public class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
        protected INavigator Navigator { get; private set; }
        protected string ControllerName { get; private set; }
        protected SimpleIoc.Contracts.IContainer Container { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void Load()
        {
            // Override in derived classes to initialize
        }

        protected virtual void Cleanup()
        {
            // Override in derived classes to perform cleanup tasks
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region IMvcViewModel members

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
