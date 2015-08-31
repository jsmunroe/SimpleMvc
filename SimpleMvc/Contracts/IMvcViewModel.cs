using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Contracts
{
    public interface IMvcViewModel
    {
        /// <summary>
        /// Navigator engine.
        /// </summary>
        INavigator Navigator { get; set; }
        
        /// <summary>
        /// Current controller's name.
        /// </summary>
        string ControllerName { get; set; }
    }
}
