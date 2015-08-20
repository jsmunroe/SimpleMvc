using SimpleMvc.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc
{
    public abstract class ControllerBase
    {
        /// <summary>
        /// Create and return a view result.
        /// </summary>
        /// <param name="a_viewName">View name.</param>
        /// <param name="a_model">View model.</param>
        /// <returns>Created view result.</returns>
        protected ViewResult View([CallerMemberName]string a_viewName = "", object a_model = null)
        {
            return new ViewResult(a_viewName, a_model);
        }
    }
}
