using SimpleIoc;
using SimpleIoc.Contracts;
using SimpleMvc.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            return _container.Resolve<TController>();
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

            return _controllerCatalog.Resolve(a_controllerName);
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

            foreach (var handler in handlers)
                handler.Handle(a_controller, a_result);
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
        /// Register the given type catalog () to use in this engine.
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
        /// Register a directory controller to use in this engine.
        /// </summary>
        /// <param name="a_assembly">Assembly in which to discover types.</param>
        /// <param name="a_directory">Relative directory in which to discover types.</param>
        /// <param name="a_suffix">Type name suffix.</param>
        /// <param name="a_baseType">Base type of </param>
        /// <returns></returns>
        public MvcEngine RegisterControllerCatalog(Assembly a_assembly, string a_directory, string a_suffix = "", Type a_baseType = null)
        {
            _controllerCatalog = new DirectoryCatalog(a_assembly, a_directory, a_suffix, a_baseType);

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
        /// Result handler paired with the type of result 
        /// </summary>
        class RegisteredHandler
        {
            public Type Handles { get; set; }
            public IResultHandler Handler { get; set; }
        }
    }
}


