using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Results
{
    public class ViewResult : ActionResult
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_viewName">View name.</param>
        /// <param name="a_model">View model.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_viewName"/> is null.</exception>
        public ViewResult(string a_viewName, object a_model)
        {
            #region Argument Validation

            if (a_viewName == null)
                throw new ArgumentNullException(nameof(a_viewName));

            #endregion

            ViewName = a_viewName;
            Model = a_model;
        }

        /// <summary>
        /// View name.
        /// </summary>
        public string ViewName { get; }

        /// <summary>
        /// View model.
        /// </summary>
        public object Model { get; }
    }
}
