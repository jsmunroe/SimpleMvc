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
        /// Mvc engine.
        /// </summary>
        MvcEngine Mvc { get; set; }
        
        /// <summary>
        /// Current controller's name.
        /// </summary>
        string ControllerName { get; set; }
    }

    public static class MvcViewModelHelpers
    {
        /// <summary>
        /// Navigate to the action with the given name (<paramref name="a_actionName"/>) within the controller with the given name (<paramref name="a_controllerName"/>).
        /// </summary>
        /// <param name="a_viewModel">"This" view model.</param>
        /// <param name="a_controllerName">Controller name.</param>
        /// <param name="a_actionName">Actor name.</param>
        /// <param name="a_routeValues">Values.</param>
        /// <exception cref="NullReferenceException">Thrown if <paramref name="a_viewModel"/> is null.</exception>
        public static void Navigate(this IMvcViewModel a_viewModel, string a_controllerName, string a_actionName, RouteDictionary a_routeValues = null)
        {
            #region Argument Validation

            if (a_viewModel == null)
                throw new NullReferenceException();

            #endregion

            a_viewModel.Mvc.Navigator.Navigate(a_controllerName, a_actionName, a_routeValues);
        }

        /// <summary>
        /// Navigate to the action with the given name (<paramref name="a_actionName"/>) within the controller with the given name (<paramref name="a_controllerName"/>).
        /// </summary>
        /// <param name="a_viewModel">"This" view model.</param>
        /// <param name="a_controllerName">Controller name.</param>
        /// <param name="a_actionName">Actor name.</param>
        /// <param name="a_routeValues">Values.</param>
        /// <exception cref="NullReferenceException">Thrown if <paramref name="a_viewModel"/> is null.</exception>
        public static void Navigate(this IMvcViewModel a_viewModel, string a_controllerName, string a_actionName, dynamic a_routeValues)
        {
            #region Argument Validation

            if (a_viewModel == null)
                throw new NullReferenceException();

            #endregion

            a_viewModel.Mvc.Navigator.Navigate(a_controllerName, a_actionName, a_routeValues);
        }

        /// <summary>
        /// Navigate to the action with the given name (<paramref name="a_actionName"/>) within the controller with the given name (<paramref name="a_controllerName"/>).
        /// </summary>
        /// <param name="a_viewModel">"This" view model.</param>
        /// <param name="a_controllerName">Controller name.</param>
        /// <param name="a_actionName">Actor name.</param>
        /// <param name="a_routeValues">Values.</param>
        /// <exception cref="NullReferenceException">Thrown if <paramref name="a_viewModel"/> is null.</exception>
        public static void Navigate(this IMvcViewModel a_viewModel, string a_actionName, RouteDictionary a_routeValues = null)
        {
            #region Argument Validation

            if (a_viewModel == null)
                throw new NullReferenceException();

            #endregion

            var controllerName = a_viewModel.ControllerName;
            a_viewModel.Mvc.Navigator.Navigate(controllerName, a_actionName, a_routeValues);
        }

        /// <summary>
        /// Navigate to the action with the given name (<paramref name="a_actionName"/>) within the controller with the given name (<paramref name="a_controllerName"/>).
        /// </summary>
        /// <param name="a_viewModel">"This" view model.</param>
        /// <param name="a_controllerName">Controller name.</param>
        /// <param name="a_actionName">Actor name.</param>
        /// <param name="a_routeValues">Values.</param>
        /// <exception cref="NullReferenceException">Thrown if <paramref name="a_viewModel"/> is null.</exception>
        public static void Navigate(this IMvcViewModel a_viewModel, string a_actionName, dynamic a_routeValues)
        {
            #region Argument Validation

            if (a_viewModel == null)
                throw new NullReferenceException();

            #endregion

            var controllerName = a_viewModel.ControllerName;
            a_viewModel.Mvc.Navigator.Navigate(controllerName, a_actionName, a_routeValues);
        }

    }
}
