using SimpleIoc;
using SimpleIoc.Contracts;
using SimpleMvc.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SimpleMvc.Exceptions;
using SimpleMvc.Handlers;
using SimpleMvc.Results;
using SimpleMvc.ViewCatalogs;

namespace SimpleMvc
{
    public class MvcEngine
    {
        private readonly Container _container;

        private ITypeCatalog _controllerCatalog;
        private ViewHandler _viewHandler;

        private readonly List<RegisteredHandler> _handlers = new List<RegisteredHandler>();

        /// <summary>
        /// Construction.
        /// </summary>
        /// <param name="a_container">Application container</param>
        public MvcEngine(Container a_container = null)
        {
            _container = a_container ?? new Container();
            Navigator= new NavigationCore(this);
        }

        /// <summary>
        /// Navigator.
        /// </summary>
        public NavigationCore Navigator { get; }

        /// <summary>
        /// Resolve an instance of the controller of the given type (<typeparamref name="TController"/>).
        /// </summary>
        /// <typeparam name="TController">Type of controller.</typeparam>
        /// <returns>Controller type.</returns>
        public TController ResolveController<TController>()
        {
            var controller = _container.Resolve<TController>();

            if (controller is ControllerBase)
                (controller as ControllerBase).Mvc = this;

            return controller;
        }

        /// <summary>
        /// Resolve an instance of the controller of the given type (<paramref name="a_controllerType"/>).
        /// </summary>
        /// <param name="a_controllerType">Type of controller.</param>
        /// <returns>Controller type.</returns>
        public object ResolveController(Type a_controllerType)
        {
            var controller = _container.Resolve(a_controllerType);

            if (controller is ControllerBase)
                (controller as ControllerBase).Mvc = this;

            return controller;
        }

        /// <summary>
        /// Resolve an instance of the controller type with the given type (<paramref name="a_controllerName"/>).
        /// </summary>
        /// <param name="a_controllerName">Controller name.</param>
        /// <returns>Controller type.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_controllerName"/> is null.</exception>
        public object ResolveController(string a_controllerName)
        {
            #region Argument Validation

            if (a_controllerName == null)
                throw new ArgumentNullException(nameof(a_controllerName));

            #endregion

            if (_controllerCatalog == null)
                throw new InvalidOperationException($"Cannot resolve the controller '{a_controllerName}' by name. No controller catalog was registered.");

            var controller = _controllerCatalog.Resolve(a_controllerName);

            if (controller is ControllerBase)
                (controller as ControllerBase).Mvc = this;

            return controller;
        }

        /// <summary>
        /// Handle the given result (<paramref name="a_result"/>).
        /// </summary>
        /// <param name="a_controller">Controller.</param>
        /// <param name="a_result">Result.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_controller"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_result"/> is null.</exception>
        public void HandleResult(object a_controller, ActionResult a_result)
        {
            #region Argument Validation

            if (a_controller == null)
                throw new ArgumentNullException(nameof(a_controller));

            if (a_result == null)
                throw new ArgumentNullException(nameof(a_result));

            #endregion

            var handlers = _handlers.Where(i => i.Handles == a_result.GetType()).Select(i => i.Handler);

            if (!handlers.Any())
                throw new MvcHandlerException($"There are no handlers registered for the type '{a_result.GetType().FullName}'.");

            var controllerName = a_controller.GetType().Name;
            if (_controllerCatalog != null)
                controllerName = _controllerCatalog.ToCatalogName(controllerName);

            foreach (var handler in handlers)
                handler.Handle(this, controllerName, a_result);
        }

        /// <summary>
        /// Register the given result handler (<paramref name="a_handler"/>) for the given type of action result (<typeparamref name="TResult"/>).
        /// </summary>
        /// <typeparam name="TResult">Type of action result.</typeparam>
        /// <param name="a_handler">Result handler.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_handler"/> is null.</exception>
        public void RegisterHandler<TResult>(IResultHandler a_handler)
            where TResult : ActionResult
        {
            #region Argument Validation

            if (a_handler == null)
                throw new ArgumentNullException(nameof(a_handler));

            #endregion

            TryBootstrap(a_handler);

            _handlers.Add(new RegisteredHandler
            {
                Handles = typeof(TResult),
                Handler = a_handler,
            });
        }

        /// <summary>
        /// Register the given type catalog (<paramref name="a_catalog"/>) to use as this engine's controller catalog.
        /// </summary>
        /// <param name="a_catalog"></param>
        /// <returns>This engine (fluent interface).</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_catalog"/> is null.</exception>
        public MvcEngine RegisterControllerCatalog(ITypeCatalog a_catalog)
        {
            #region Argument Validation

            if (a_catalog == null)
                throw new ArgumentNullException(nameof(a_catalog));

            #endregion

            _controllerCatalog = a_catalog;

            return this;
        }

        /// <summary>
        /// Register a directory catalog to use as this engine's controller catalog.
        /// </summary>
        /// <param name="a_directory">Relative directory in which to discover types.</param>
        /// <param name="a_suffix">Type name suffix.</param>
        /// <param name="a_baseType">Base type of </param>
        /// <returns>This engine (fluent interface).</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public MvcEngine RegisterControllerCatalog(string a_directory, string a_suffix = "", Type a_baseType = null)
        {
            var assembly = Assembly.GetCallingAssembly();
            _controllerCatalog = new DirectoryCatalog(_container, assembly, a_directory, a_suffix, a_baseType ?? typeof(ControllerBase));

            return this;
        }

        /// <summary>
        /// Register the given type catalog (<paramref name="a_catalog"/>) to use as this engine's view catalog.
        /// </summary>
        /// <param name="a_catalog"></param>
        /// <returns></returns>
        public MvcEngine RegisterViewCatalog(ITypeCatalog a_catalog)
        {
            var viewHandler = GetViewHandler();

            viewHandler.RegisterViewCatalog(a_catalog);

            return this;
        }

        /// <summary>
        /// Register a directory catalog to use as this engine's view catalog.
        /// </summary>
        /// <param name="a_directory">Relative directory in which to discover types.</param>
        /// <param name="a_suffix">Type name suffix.</param>
        /// <param name="a_baseType">Base type of </param>
        /// <returns>This engine (fluent interface).</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public MvcEngine RegisterViewCatalog(string a_directory, string a_suffix = "", Type a_baseType = null)
        {
            var assembly = Assembly.GetCallingAssembly();
            var catalog = new DirectoryCatalog(_container, assembly, a_directory, a_suffix, a_baseType);

            var viewHandler = GetViewHandler();

            viewHandler.RegisterViewCatalog(catalog);

            return this;
        }

        /// <summary>
        /// Register the given view target (<paramref name="a_viewTarget"/>) with this engine's view handling system.
        /// </summary>
        /// <param name="a_viewTarget">View target.</param>
        /// <returns>This engine (fluent interface).</returns>
        public MvcEngine RegisterViewTarget(IViewTarget a_viewTarget)
        {
            var viewHandler = GetViewHandler();
            viewHandler.RegisterViewTarget(a_viewTarget);

            return this;
        }

        /// <summary>
        /// Register the given model binder (<paramref name="a_modelBinder"/>) with this engine's view handling system.
        /// </summary>
        /// <param name="a_modelBinder">View target.</param>
        /// <returns>This engine (fluent interface).</returns>
        public MvcEngine RegisterModelBinder(IModelBinder a_modelBinder)
        {
            var viewHandler = GetViewHandler();
            viewHandler.RegisterModelBinder(a_modelBinder);

            return this;
        }

        /// <summary>
        /// Register the given result handler (<paramref name="a_handler"/>) for the given type of action result (<typeparamref name="TResult"/>).
        /// </summary>
        /// <typeparam name="TResult">Type of action result.</typeparam>
        /// <param name="a_handler">Result handler.</param>
        /// <returns>This engine (fluent interface).</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_handler"/> is null.</exception>
        public MvcEngine RegisterHandler<TResult>(ResultHandlerBase<TResult> a_handler)
            where TResult : ActionResult
        {
            #region Argument Validation

            if (a_handler == null)
                throw new ArgumentNullException(nameof(a_handler));

            #endregion

            TryBootstrap(a_handler);

            _handlers.Add(new RegisteredHandler
            {
                Handles = typeof(TResult),
                Handler = a_handler,
            });

            return this;
        }

        /// <summary>
        /// If the given instance (<paramref name="a_instance"/>) is bootstrappable and hasn't yet been bootstrapped, then bootstrap it against the container herein (<see cref="_container"/>).
        /// </summary>
        /// <param name="a_instance">Instance to bootstrap.</param>
        private void TryBootstrap(object a_instance)
        {
            var bootstrappable = a_instance as IBootstrappable;
            if (bootstrappable == null || bootstrappable.IsBootstrapped)
                return;

            bootstrappable.Bootstrap(_container);
        }

        /// <summary>
        /// Get the view handler herein. Create and register it if it is not yet available.
        /// </summary>
        /// <returns>View handler.</returns>
        private ViewHandler GetViewHandler()
        {
            if (_viewHandler == null)
            {
                _viewHandler = new ViewHandler();
                RegisterHandler(_viewHandler);
            }

            return _viewHandler;
        }


        /// <summary>
        /// Result handler paired with the type of result 
        /// </summary>
        class RegisteredHandler
        {
            public Type Handles { get; set; }
            public IResultHandler Handler { get; set; }
        }

    }
}


