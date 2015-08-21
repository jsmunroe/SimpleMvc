using System;
using System.Collections.Generic;
using SimpleIoc;
using SimpleIoc.Contracts;
using SimpleMvc.Contracts;
using SimpleMvc.Exceptions;
using SimpleMvc.Results;

namespace SimpleMvc.Handlers
{
    public class ViewHandler : ResultHandlerBase<ViewResult>, IBootstrappable
    {
        protected IViewCatalog _viewCatalog;

        private readonly List<IViewTarget> _viewTargets = new List<IViewTarget>();

        /// <summary>
        /// Whether this handler has been bootstrapped.
        /// </summary>
        public bool IsBootstrapped { get; private set; }

        /// <summary>
        /// Handle the given result (<paramref name="a_result"/>).
        /// </summary>
        /// <param name="a_result">Result to handle.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_result"/> is null.</exception>
        public override void Handle(ViewResult a_result)
        {
            #region Argument Validation

            if (a_result == null)
                throw new ArgumentNullException(nameof(a_result));

            #endregion

            // Get view object from view catalog.
            var view = _viewCatalog.GetView(a_result.ViewName, a_result.Model);

            if (view == null)
                throw new ViewNotFoundException(a_result.ViewName);

            // Send view object to view targets.
            foreach (var viewTarget in _viewTargets)
                viewTarget.SetView(view);
        }

        /// <summary>
        /// Bootstrap this handler against the given container (<paramref name="a_container"/>).
        /// </summary>
        /// <param name="a_container">Container.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_container"/> is null.</exception>
        public virtual void Bootstrap(Container a_container)
        {
            #region Argument Validation

            if (a_container == null)
                throw new ArgumentNullException(nameof(a_container));

            #endregion

            IsBootstrapped = true;
        }

        /// <summary>
        /// Register the given view catalog for this handler.
        /// </summary>
        /// <param name="a_viewCatalog">View catalog.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_viewCatalog"/> is null.</exception>
        public void RegisterViewCatalog(IViewCatalog a_viewCatalog)
        {
            #region Argument Validation

            if (a_viewCatalog == null)
                throw new ArgumentNullException(nameof(a_viewCatalog));

            #endregion

            _viewCatalog = a_viewCatalog;
        }

        /// <summary>
        /// Register the given view target for this handler.
        /// </summary>
        /// <param name="a_viewTarget">View target.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_viewTarget"/> is null.</exception>
        public void RegisterViewTarget(IViewTarget a_viewTarget)
        {
            #region Argument Validation

            if (a_viewTarget == null)
                throw new ArgumentNullException(nameof(a_viewTarget));

            #endregion

            if (_viewTargets.Contains(a_viewTarget))
                throw new InvalidOperationException("View target already exists in this handler.");

            _viewTargets.Add(a_viewTarget);
        }
    }
}