using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SimpleIoc;
using SimpleMvc.Contracts;
using SimpleMvc.Exceptions;

namespace SimpleMvc.ViewCatalogs
{
    public class DirectoryViewCatalog : IViewCatalog
    {
        private readonly Container _container;
        private readonly Assembly _assembly;

        private Dictionary<string, Type> _typesByName;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_container">Container.</param>
        /// <param name="a_assembly">Assembly in which to discover views.</param>
        /// <param name="a_directory">Relative directory in which to discover views.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_container"/>" is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_assembly"/>" is null.</exception>
        public DirectoryViewCatalog(Container a_container, Assembly a_assembly, string a_directory)
        {
            #region Argument Validation

            if (a_assembly == null)
                throw new ArgumentNullException(nameof(a_assembly));

            if (a_directory == null)
                throw new ArgumentNullException(nameof(a_directory));

            if (a_container == null)
                throw new ArgumentNullException(nameof(a_container));

            #endregion

            _container = a_container;
            _assembly = a_assembly;

            var relative = Regex.Replace(a_directory, @"[/\\]", ".").TrimStart('.');
            var match = Regex.Match(_assembly.FullName, @"^[^,]*");
            Namespace = match.Value + "." + relative;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_assembly">Assembly in which to discover views.</param>
        /// <param name="a_directory">Relative directory in which to discover views.</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_assembly"/>" is null.</exception>
        public DirectoryViewCatalog(Assembly a_assembly, string a_directory)
        {
            #region Argument Validation

            if (a_assembly == null)
                throw new ArgumentNullException(nameof(a_assembly));

            if (a_directory == null)
                throw new ArgumentNullException(nameof(a_directory));

            #endregion

            _container = new Container();
            _assembly = a_assembly;

            var relative = Regex.Replace(a_directory, @"[/\\]", ".").TrimStart('.');
            var match = Regex.Match(_assembly.FullName, @"^[^,]*");
            Namespace = match.Value + "." + relative;
        }

        /// <summary>
        /// Source namespace for this catalog.
        /// </summary>
        public string Namespace { get; }

        /// <summary>
        /// Load the types within 
        /// </summary>
        private void LoadTypes()
        {
            var types = from type in _assembly.GetTypes()
                where type.Namespace?.StartsWith(Namespace) ?? false
                select type;

            _typesByName = types.ToDictionary(i => i.Name);
        }

        /// <summary>
        /// Get a view with the given view name (<paramref name="a_viewName"/>).
        /// </summary>
        /// <param name="a_viewName">View name.</param>
        public object GetView(string a_viewName)
        {
            if (_typesByName == null)
                LoadTypes();

            if (_typesByName == null || !_typesByName.ContainsKey(a_viewName))
                throw new ViewNotFoundException(a_viewName);

            var viewType = _typesByName[a_viewName];

            return _container.Resolve(viewType);
        }
    }
}