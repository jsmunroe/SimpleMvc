using SimpleIoc.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Contracts
{
    public interface IViewModel
    {
        /// <summary>
        /// Navigator engine.
        /// </summary>
        INavigator Navigator { get; set; }

        /// <summary>
        /// Current controller's name.
        /// </summary>
        string ControllerName { get; set; }

        /// <summary>
        /// Root container of the MVC application.
        /// </summary>
        IContainer Container { get; set; }

        /// <summary>
        /// Loads the necessary resources or data required for operation.
        /// </summary>
        void Load();

        /// <summary>
        /// Cleanup this view model. 
        /// </summary>
        void Cleanup();
    }
}
