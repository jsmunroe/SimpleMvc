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
        void Navigate<TController>(string a_actionName, RouteDictionary a_routeValues);

        /// <summary>
        /// Navigate to the action with the given name (<paramref name="a_actionName"/>) within the controller with the given name (<paramref name="a_controllerName"/>).
        /// </summary>
        /// <param name="a_controllerName">Controller name.</param>
        /// <param name="a_actionName">Actor name.</param>
        /// <param name="a_routeValues">Values.</param>
        void Navigate(string a_controllerName, string a_actionName, dynamic a_routeValues);

        /// <summary>
        /// Navigate to the action 
        /// </summary>
        /// <typeparam name="TController">Type of controller.</typeparam>
        /// <param name="a_actionName">Action name.</param>
        /// <param name="a_routeValues">Values.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_actionName"/>" is null.</exception>
        void Navigate<TController>(string a_actionName, dynamic a_routeValues);

        /// <summary>
        /// Navigate using the given action expression (<paramref name="a_actionExpression"/>) against the controller of the given type (<typeparamref name="TController"/>).
        /// </summary>
        /// <typeparam name="TController">Type of controller.</typeparam>
        /// <param name="a_actionExpression">Action expression.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_actionExpression"/>" is null.</exception>
        void Navigate<TController>(Expression<ActionCall<TController>> a_actionExpression);
    }
}