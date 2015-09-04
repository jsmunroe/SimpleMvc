using System;
using SimpleMvc.Contracts;
using SimpleMvc.Results;

namespace SimpleMvc.Handlers
{
    public class RedirectHandler : ResultHandlerBase<RedirectResult>
    {
        private readonly INavigator _navigator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_navigator">Navigator.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_navigator"/> is null.</exception>
        public RedirectHandler(INavigator a_navigator)
        {
            #region Argument Validation

            if (a_navigator == null)
                throw new ArgumentNullException(nameof(a_navigator));

            #endregion

            _navigator = a_navigator;
        }

        /// <summary>
        /// Handle the given result (<paramref name="a_result"/>).
        /// </summary>
        /// <param name="a_mvc"></param>
        /// <param name="a_controllerName"></param>
        /// <param name="a_result">Result to handle.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_controllerName"/> is null.</exception>
        public override void Handle(MvcEngine a_mvc, string a_controllerName, RedirectResult a_result)
        {
            #region Argument Validation

            if (a_controllerName == null)
                throw new ArgumentNullException(nameof(a_controllerName));

            if (a_result == null)
                throw new ArgumentNullException(nameof(a_result));

            #endregion

            _navigator.Navigate(
                a_result.ControllerName ?? a_controllerName, 
                a_result.ActionName, 
                a_result.Values);
        }
    }
}