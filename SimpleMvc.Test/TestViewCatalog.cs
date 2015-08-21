using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc;
using SimpleMvc.Contracts;

namespace SimpleMvc.Test
{
    public class TestViewCatalog : IViewCatalog
    {
        private readonly Container _container = new Container();
        private readonly Dictionary<string, Type> _viewTypesByName = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        public List<string> ViewNames { get; private set; } = new List<string>();

        /// <summary>
        /// Register the given type of view (<typeparamref name="TView"/>) under the given view name (<paramref name="a_viewName"/>).
        /// </summary>
        /// <typeparam name="TView">Type of view.</typeparam>
        /// <param name="a_viewName">View name.</param>
        public void RegisterView<TView>(string a_viewName)
        {
            _viewTypesByName[a_viewName] = typeof (TView);
        }

        /// <summary>
        /// Get a view with the given view name (<paramref name="a_viewName"/>).
        /// </summary>
        /// <param name="a_viewName">View name.</param>
        /// <param name="a_model">Model to apply to the view.</param>
        public object GetView(string a_viewName, object a_model)
        {
            #region Argument Validation

            if (a_viewName == null)
                throw new ArgumentNullException(nameof(a_viewName));

            #endregion

            if (!_viewTypesByName.ContainsKey(a_viewName))
                return null;

            var viewType = _viewTypesByName[a_viewName];
            var view = _container.Resolve(viewType);

            if (view is TestViewBase)
                (view as TestViewBase).DataModel = a_model;

            ViewNames.Add(a_viewName);

            return view;
        }
    }
}
