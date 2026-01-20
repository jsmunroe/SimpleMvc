using SimpleMvc.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SimpleMvc;

namespace SimpleMvc
{
    public abstract class ControllerBase
    {
        /// <summary>
        /// Mvc engine.
        /// </summary>
        public MvcEngine Mvc { get; internal set; }

        /// <summary>
        /// Create and return a view result.
        /// </summary>
        /// <param name="a_model">View model.</param>
        /// <param name="a_viewName">View name.</param>
        /// <returns>Created view result.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_viewName"/> is null.</exception>
        protected ViewResult View(object a_model, [CallerMemberName] string a_viewName = "")
        {
            return new ViewResult
            {
                ViewName = a_viewName,
                Model = a_model,
            };
        }

        /// <summary>
        /// Create and return a view result.
        /// </summary>
        /// <param name="a_viewName">View name.</param>
        /// <returns>Created view result.</returns>
        protected ViewResult View([CallerMemberName] string a_viewName = "")
        {
            return new ViewResult
            {
                ViewName = a_viewName,
                Model = null,
            };

        }

        /// <summary>
        /// Redirect to another action within this controller.
        /// </summary>
        /// <param name="a_actionName">Action name.</param>
        /// <param name="a_values">Route values.</param>
        /// <returns>Created redirect result.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_actionName"/> is null.</exception>
        protected RedirectResult Redirect(string a_actionName, RouteDictionary a_values = null)
        {
            #region Argument Validation

            if (a_actionName == null)
                throw new ArgumentNullException(nameof(a_actionName));

            #endregion

            return new RedirectResult
            {
                ActionName = a_actionName,
                Values = a_values ?? new RouteDictionary(),
            };
        }

        /// <summary>
        /// Redirect to another action within this controller.
        /// </summary>
        /// <param name="a_actionName">Action name.</param>
        /// <param name="a_values">Route values.</param>
        /// <returns>Created redirect result.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_actionName"/> is null.</exception>
        protected RedirectResult Redirect(string a_actionName, dynamic a_values)
        {
            #region Argument Validation

            if (a_actionName == null)
                throw new ArgumentNullException(nameof(a_actionName));

            #endregion

            return new RedirectResult
            {
                ActionName = a_actionName,
                Values = new RouteDictionary(a_values),
            };
        }

        /// <summary>
        /// Redirect to another action within this controller.
        /// </summary>
        /// <param name="a_actionName">Action name.</param>
        /// <param name="a_controllerName">Controller name.</param>
        /// <param name="a_values">Route values.</param>
        /// <returns>Created redirect result.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_actionName"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_controllerName"/> is null.</exception>
        protected RedirectResult Redirect(string a_actionName, string a_controllerName, RouteDictionary a_values = null)
        {
            #region Argument Validation

            if (a_actionName == null)
                throw new ArgumentNullException(nameof(a_actionName));

            if (a_controllerName == null)
                throw new ArgumentNullException(nameof(a_controllerName));

            #endregion


            return new RedirectResult
            {
                ControllerName = a_controllerName,
                ActionName = a_actionName,
                Values = a_values ?? new RouteDictionary(),
            };
        }

        /// <summary>
        /// Redirect to another action within this controller.
        /// </summary>
        /// <param name="a_actionName">Action name.</param>
        /// <param name="a_controllerName">Controller name.</param>
        /// <param name="a_values">Route values.</param>
        /// <returns>Created redirect result.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_actionName"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_controllerName"/> is null.</exception>
        protected RedirectResult Redirect(string a_actionName, string a_controllerName, dynamic a_values)
        {
            #region Argument Validation

            if (a_actionName == null)
                throw new ArgumentNullException(nameof(a_actionName));

            if (a_controllerName == null)
                throw new ArgumentNullException(nameof(a_controllerName));

            #endregion

            return new RedirectResult
            {
                ControllerName = a_controllerName,
                ActionName = a_actionName,
                Values = new RouteDictionary(a_values),
            };
        }
    }
}
