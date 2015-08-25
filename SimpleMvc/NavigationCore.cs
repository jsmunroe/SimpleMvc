using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleIoc;
using SimpleMvc.Results;

namespace SimpleMvc
{
    public class NavigationCore
    {
        private readonly MvcEngine _mvc = null;

        /// <summary>
        /// Construction.
        /// </summary>
        /// <param name="a_mvc">Parent MVC engine.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_mvc"/> is null.</exception>
        public NavigationCore(MvcEngine a_mvc)
        {
            #region Argument Validation

            if (a_mvc == null)
                throw new ArgumentNullException(nameof(a_mvc));

            #endregion

            _mvc = a_mvc;
        }

        /// <summary>
        /// Navigate to the action with the given name (<paramref name="a_actionName"/>) within the controller with the given name (<paramref name="a_controllerName"/>).
        /// </summary>
        /// <param name="a_controllerName">Controller name.</param>
        /// <param name="a_actionName">Actor name.</param>
        /// <param name="a_routeValues">Values.</param>
        public void Navigate(string a_controllerName, string a_actionName, RouteDictionary a_routeValues)
        {
            
        }

        /// <summary>
        /// Navigate to the action 
        /// </summary>
        /// <typeparam name="TController">Type of controller.</typeparam>
        /// <param name="a_actionName">Action name.</param>
        /// <param name="a_routeValues">Values.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_actionName"/>" is null.</exception>
        public void Navigate<TController>(string a_actionName, RouteDictionary a_routeValues)
        {
            #region Argument Validation

            if (a_actionName == null)
                throw new ArgumentNullException(nameof(a_actionName));

            #endregion

            a_routeValues = a_routeValues ?? new RouteDictionary();

            var controller = _mvc.ResolveController<TController>();

            var actionExpression = CreateActionExpression(controller, a_actionName, a_routeValues);

            Navigate(actionExpression);
        }

        /// <summary>
        /// Navigate using the given action expression (<paramref name="a_actionExpression"/>) against the controller of the given type (<typeparamref name="TController"/>).
        /// </summary>
        /// <typeparam name="TController">Type of controller.</typeparam>
        /// <param name="a_actionExpression">Action expression.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_actionExpression"/>" is null.</exception>
        public void Navigate<TController>(Expression<ActionCall<TController>> a_actionExpression)
        {
            #region Argument Validation

            if (a_actionExpression == null)
                throw new ArgumentNullException(nameof(a_actionExpression));

            #endregion

            var controller = _mvc.ResolveController<TController>();

            var result = a_actionExpression.Compile()(controller);

            // Process the action result.

            _mvc.HandleResult(controller, result);
        }

        /// <summary>
        /// Create a lambda expression tree for the action with the given name (<paramref name="a_actionName"/>) and route values ().
        /// </summary>
        /// <typeparam name="TController">Type of controller.</typeparam>
        /// <param name="a_controller">Controller.</param>
        /// <param name="a_actionName">Action name.</param>
        /// <param name="a_routeValues">Route values.</param>
        /// <returns>Created expression.</returns>
        private static Expression<ActionCall<TController>> CreateActionExpression<TController>(TController a_controller, string a_actionName, RouteDictionary a_routeValues)
        {
            // Get all action methods in the controler that match the given action name.
            var controllerType = a_controller.GetType();
            var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                        .Where(i => i.Name.Equals(a_actionName, StringComparison.OrdinalIgnoreCase))
                                        .Where(i => i.ReturnType.IsAssignableFrom(typeof(ActionResult)))
                                        .ToArray();

            // Find the method for which all of the required parameters are provided in the route values.
            MethodInfo actionMethod = null;
            var values = new List<object>();
            foreach (var method in methods)
            {
                values.Clear();
                var parameters = method.GetParameters();
                var found = true;
                foreach (var parameter in parameters)
                {
                    object value;
                    if (a_routeValues.TryGetValue(parameter.Name, out value))
                    {
                        // Parameter name found in route values, check types.
                        var paramType = parameter.ParameterType;
                        if ((value == null && !paramType.IsClass) || (value != null && !paramType.IsInstanceOfType(value)))
                            found = false; // Signature mismatch.
                    }
                    else
                    {
                        // Prameter name not found in route values, check for default value.
                        if (parameter.HasDefaultValue)
                            value = parameter.DefaultValue;
                        else
                            found = false; // Signature mismatch.
                    }

                    if (!found)
                        break;

                    values.Add(value);
                }

                if (found)
                {
                    actionMethod = method;
                    break;
                }
            }

            if (actionMethod == null)
                throw new Exceptions.NavigationException($"Cannot resolve an action with the given signature (Name='{a_actionName}').");

            // Create the lambda expresion.
            var paramExpressions = values.Select(i => (Expression)Expression.Constant(i)).ToArray();

            var actionExpression = Expression.Lambda<ActionCall<TController>>(
                                        Expression.Call(
                                            Expression.Constant(a_controller),
                                            actionMethod,
                                            paramExpressions),
                                        Expression.Parameter(typeof(TController)));
            return actionExpression;
        }

    }

    public delegate ActionResult ActionCall<in TController>(TController a_controller);

}