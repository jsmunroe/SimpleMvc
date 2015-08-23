using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SimpleMvc.Wpf
{
    public static class UiDispatcherFetcher
    {
        private static Dispatcher _current = null;

        /// <summary>
        /// User interface dispatcher.
        /// </summary>
        public static Dispatcher Current
        {
            get
            {
                if (_current == null)
                    _current = Application.Current != null ? Application.Current.Dispatcher : Dispatcher.CurrentDispatcher;

                return _current;
            }
            internal set
            {
                _current = value;
            }
        }
    }
}
