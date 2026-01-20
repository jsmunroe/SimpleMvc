using System;
using System.Linq.Expressions;

namespace SimpleMvc.Contracts
{
    public interface INavigator
    {
        /// <summary>
        /// Navigate to the action with the given name (<paramref name="a_actionName"/>) within the controller with the given name (<paramref name="a_controllerName"/>).
        /// </summary>
        /// <param name="a_controllerName">Controller name.</param>
        /// <param name="a_actionName">Actor name.</param>
        /// <param name="a_routeValues">Values.</param>
        void Navigate(string a_controllerName, string a_actionName, RouteDictionary a_routeValues);

        /// <summary>
        /// Navigate to the action 
        /// </summary>
        /// <typeparam name="TController">Type of controller.</typeparam>
        /// <param name="a_actionName">Action name.</param>
        /// <param name="a_routeValues">Values.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_actionName"/>" is null.</exception>
        void Navigate<TController>(string a_actionName, RouteDictionary a_routeValues = null);

        /// <summary>
        /// Navigate to the action with the given name (<paramref name="a_actionName"/>) within the controller with the given name (<paramref name="a_controllerName"/>).
        /// </summary>
        /// <param name="a_controllerName">Controller name.</param>
        /// <param name="a_actionName">Actor name.</param>
        /// <param name="a_routeValues">Values.</param>
        void Navigate(string a_controllerName, string a_actionName, dynamic a_routeValues = null);

        /// <summary>
        /// Navigate to the action 
        /// </summary>
        /// <typeparam name="TController">Type of controller.</typeparam>
        /// <param name="a_actionName">Action name.</param>
        /// <param name="a_routeValues">Values.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_actionName"/>" is null.</exception>
        void Navigate<TController>(string a_actionName, dynamic a_routeValues = null);

        /// <summary>
        /// Navigate using the given action expression (<paramref name="a_actionExpression"/>) against the controller of the given type (<typeparamref name="TController"/>).
        /// </summary>
        /// <typeparam name="TController">Type of controller.</typeparam>
        /// <param name="a_actionExpression">Action expression.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_actionExpression"/>" is null.</exception>
        void Navigate<TController>(Expression<ActionCall<TController>> a_actionExpression);

        /// <summary>
        /// Navigates to the view associated with the specified controller and view model types.
        /// </summary>
        /// <typeparam name="TController">The type of the controller to navigate to. Must be a valid controller type registered in the navigation
        /// system.</typeparam>
        /// <typeparam name="TViewModel">The type of the view model to use for the target view. Must be compatible with the specified controller.</typeparam>
        void Navigate<TController, TViewModel>();

        /// <summary>
        /// Navigates to the specified controller and displays the view associated with the given view model type.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model to display. Must be a class that represents the data and logic for the target
        /// view.</typeparam>
        /// <param name="a_controllerName">The name of the controller to navigate to. Cannot be null or empty.</param>
        void Navigate<TViewModel>(string a_controllerName);

    }
}