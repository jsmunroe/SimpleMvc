using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Wpf.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class ViewModelAttribute : Attribute
    {
        public ViewModelAttribute(string controllerName, string actionName)
        {
            ControllerName = controllerName;
            ActionName = actionName;
        }

        public ViewModelAttribute(string actionName)
        {
            ControllerName = null;
            ActionName = actionName;
        }

        public string ControllerName { get; }
        public string ActionName { get; }
    }
}
