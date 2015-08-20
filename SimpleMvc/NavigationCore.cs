using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleIoc;

namespace SimpleMvc
{
    public class NavigationCore
    {
        private readonly Container _container;

        /// <summary>
        /// Construction.
        /// </summary>
        /// <param name="a_container">Application container</param>
        public NavigationCore(Container a_container = null)
        {
            _container = a_container ?? new Container();
        }

        /// <summary>
        /// Navigate to the action 
        /// </summary>
        /// <typeparam name="TController">Type of controller.</typeparam>
        /// <param name="a_actionName">Action name.</param>
        /// <param name="a_routeValues">Values.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_actionName"/>" is null.</exception>
        public void Navigate<TController>(string a_actionName, RouteDictionary a_routeValues)
            where TController : ControllerBase
        {
            #region Argument Validation

            if (a_actionName == null)
                throw new ArgumentNullException(nameof(a_actionName));

            #endregion

            a_routeValues = a_routeValues ?? new RouteDictionary();

            var controller = _container.Resolve<TController>();

            var controllerType = controller.GetType();
            var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                        .Where(i => i.Name.Equals(a_actionName, StringComparison.OrdinalIgnoreCase))
                                        .ToArray();

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
                    if (!a_routeValues.TryGetValue(parameter.Name, out value))
                    {
                        if (parameter.HasDefaultValue)
                        {
                            value = parameter.DefaultValue;
                        }
                        else
                        {
                            found = false;
                            break;
                        }
                    }

                    var paramType = parameter.ParameterType;
                    if ((value == null && !paramType.IsClass) || (value != null && !paramType.IsInstanceOfType(value)))
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    actionMethod = method;
                    break;
                }
            }

            if (actionMethod == null)
                throw new NavigationException($"Cannot resolve an action with the given signature (Name='{a_actionName}').");

            var paramExpressions = values.Select(i => (Expression)Expression.Constant(i)).ToArray();

            var actionExpression = Expression.Lambda<ActionCall<TController>>(
                                        Expression.Call(
                                            Expression.Constant(controller),
                                            actionMethod, 
                                            paramExpressions),
                                        Expression.Parameter(typeof(TController)));

            Navigate(actionExpression);
        }

        /// <summary>
        /// Navigate using the given action expression (<paramref name="a_actionExpression"/>) against the controller of the given type (<typeparamref name="TController"/>).
        /// </summary>
        /// <typeparam name="TController">Type of controller.</typeparam>
        /// <param name="a_actionExpression">Action expression.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_actionExpression"/>" is null.</exception>
        public void Navigate<TController>(Expression<ActionCall<TController>> a_actionExpression)
            where TController : ControllerBase
        {
            #region Argument Validation

            if (a_actionExpression == null)
                throw new ArgumentNullException(nameof(a_actionExpression));

            #endregion

            var controller = _container.Resolve<TController>();

            var result = a_actionExpression.Compile()(controller);

            // TODO: Process the action result.
        }
    }

    public class NavigationException : Exception
    {
        public NavigationException(string a_message)
            : base(a_message)
        {
            
        }

        public NavigationException(string a_message, Exception a_innerException)
            : base(a_message, a_innerException)
        {

        }
    }

    public delegate ActionResult ActionCall<in TController>(TController a_controller);

    /// <summary>
    /// Case-insensitive dictionary of named route values.
    /// </summary>
    public class RouteDictionary : Dictionary<string, object>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RouteDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
            
        }
    }
}