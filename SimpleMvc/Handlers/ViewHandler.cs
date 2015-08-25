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
        protected ITypeCatalog _viewCatalog;

        private IModelBinder _modelBinder;

        private readonly List<IViewTarget> _viewTargets = new List<IViewTarget>();

        /// <summary>
        /// Whether this handler has been bootstrapped.
        /// </summary>
        public bool IsBootstrapped { get; private set; }

        /// <summary>
        /// Handle the given result (<paramref name="a_result"/>).
        /// </summary>
        /// <param name="a_controller"></param>
        /// <param name="a_result">Result to handle.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_result"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_controller"/> is null.</exception>
        public override void Handle(object a_controller, ViewResult a_result)
        {
            #region Argument Validation

            if (a_result == null)
                throw new ArgumentNullException(nameof(a_result));

            if (a_controller == null)
                throw new ArgumentNullException(nameof(a_controller));

            #endregion

            var controllerName = a_controller.GetType().Name;

            // Get view object from view catalog.
            var view = _viewCatalog.Resolve(a_result.ViewName);

            if (view == null)
                throw new TypeNotFoundException(a_result.ViewName);

            // Apply model to the view.
            _modelBinder?.Bind(view, a_result.Model);


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
        public void RegisterViewCatalog(ITypeCatalog a_viewCatalog)
        {
            #region Argument Validation

            if (a_viewCatalog == null)
                throw new ArgumentNullException(nameof(a_viewCatalog));

            #endregion

            _viewCatalog = a_viewCatalog;
        }

        /// <summary>
        /// Register the given view target (<paramref name="a_viewTarget"/>) for this handler.
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

        /// <summary>
        /// Register the given model binder (<paramref name="a_modelBinder"/>) for this handler.
        /// </summary>
        /// <param name="a_modelBinder">Model binder.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_modelBinder"/>" is null.</exception>
        public void RegisterModelBinder(IModelBinder a_modelBinder)
        {
            #region Argument Validation

            if (a_modelBinder == null)
                throw new ArgumentNullException(nameof(a_modelBinder));

            #endregion

            _modelBinder = a_modelBinder;
        }
    }
}