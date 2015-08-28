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
    public class DirectoryCatalog : ITypeCatalog
    {
        private readonly Container _container;
        private readonly Assembly _assembly;
        private readonly string _suffix;
        private readonly Type _baseType;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_container">Container.</param>
        /// <param name="a_assembly">Assembly in which to discover types.</param>
        /// <param name="a_directory">Relative directory in which to discover types.</param>
        /// <param name="a_suffix">Type name suffix.</param>
        /// <param name="a_baseType">Base type of </param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_container"/>" is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_assembly"/>" is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_suffix"/> is null.</exception>
        public DirectoryCatalog(Container a_container, Assembly a_assembly, string a_directory, string a_suffix = "", Type a_baseType = null)
        {
            #region Argument Validation

            if (a_assembly == null)
                throw new ArgumentNullException(nameof(a_assembly));

            if (a_directory == null)
                throw new ArgumentNullException(nameof(a_directory));

            if (a_container == null)
                throw new ArgumentNullException(nameof(a_container));

            if (a_suffix == null)
                throw new ArgumentNullException(nameof(a_suffix));

            #endregion

            _container = a_container;
            _assembly = a_assembly;
            _suffix = a_suffix;
            _baseType = a_baseType ?? typeof (object);

            var relative = Regex.Replace(a_directory, @"[/\\]", ".").TrimStart('.');
            var match = Regex.Match(_assembly.FullName, @"^[^,]*");
            Namespace = match.Value + "." + relative;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_assembly">Assembly in which to discover types.</param>
        /// <param name="a_directory">Relative directory in which to discover types.</param>
        /// <param name="a_suffix">Type name suffix.</param>
        /// <param name="a_baseType">Base type of </param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_assembly"/>" is null.</exception>
        public DirectoryCatalog(Assembly a_assembly, string a_directory, string a_suffix = "", Type a_baseType = null)
        {
            #region Argument Validation

            if (a_assembly == null)
                throw new ArgumentNullException(nameof(a_assembly));

            if (a_directory == null)
                throw new ArgumentNullException(nameof(a_directory));

            if (a_suffix == null)
                throw new ArgumentNullException(nameof(a_suffix));

            #endregion

            _container = new Container();
            _assembly = a_assembly;
            _suffix = a_suffix;
            _baseType = a_baseType ?? typeof(object);

            var relative = Regex.Replace(a_directory, @"[/\\]", ".").TrimStart('.');
            var match = Regex.Match(_assembly.FullName, @"^[^,]*");
            Namespace = match.Value + "." + relative;
        }

        /// <summary>
        /// Source namespace for this catalog.
        /// </summary>
        public string Namespace { get; }

        /// <summary>
        /// Get a type with the given type name (<paramref name="a_catalogName"/>).
        /// </summary>
        /// <param name="a_catalogName">Type name.</param>
        /// <returns>Resolved instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_catalogName"/> is null.</exception>
        public object Resolve(string a_catalogName)
        {
            #region Argument Validation

            if (a_catalogName == null)
                throw new ArgumentNullException(nameof(a_catalogName));

            #endregion

            var types = from type in _assembly.GetTypes()
                        where FilterTypesByName(type, a_catalogName)
                        select type;

            if (types.Take(2).Count() != 1)
                throw new TypeNotFoundException(a_catalogName);

            var resolvedType = types.Single();

            return _container.Resolve(resolvedType);
        }

        /// <summary>
        /// Get a type with the given type name () in the given namespace subbranch ().
        /// </summary>
        /// <param name="a_sub">Namespace subbranch.</param>
        /// <param name="a_typeName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_sub"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_typeName"/> is null.</exception>
        public object Resolve(string a_sub, string a_typeName)
        {
            #region Argument Validation

            if (a_sub == null)
                throw new ArgumentNullException(nameof(a_sub));

            if (a_typeName == null)
                throw new ArgumentNullException(nameof(a_typeName));

            #endregion

            var fullName = string.Join(".", a_sub, a_typeName).Trim('.');

            return Resolve(fullName);
        }

        /// <summary>
        /// Get the catalog name from the given type name (<paramref name="a_typeName"/>).
        /// </summary>
        /// <param name="a_typeName">Type name.</param>
        /// <returns>Catalog name.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_typeName"/> is null.</exception>
        public string ToCatalogName(string a_typeName)
        {
            #region Argument Validation

            if (a_typeName == null)
                throw new ArgumentNullException(nameof(a_typeName));

            #endregion

            if (!a_typeName.EndsWith(_suffix))
                return a_typeName;

            return a_typeName.Substring(0, a_typeName.Length - _suffix.Length);
        }

        /// <summary>
        /// Get the type name from the given catalog name (<paramref name="a_catalogName"/>).
        /// </summary>
        /// <param name="a_catalogName">Catalog name.</param>
        /// <returns>Type name.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_typeName"/> is null.</exception>
        public string ToTypeName(string a_catalogName)
        {
            #region Argument Validation

            if (a_catalogName == null)
                throw new ArgumentNullException(nameof(a_catalogName));

            #endregion

            return a_catalogName + _suffix;
        }

        /// <summary>
        /// Filter the given type (<paramref name="a_type"/>) by the given type name (<paramref name="a_typeName"/>).
        /// </summary>
        /// <param name="a_type">Type.</param>
        /// <param name="a_typeName">Type name.</param>
        /// <returns>True if type matches name.</returns>
        private bool FilterTypesByName(Type a_type, string a_typeName)
        {
            if (!a_type.Namespace?.StartsWith(Namespace) ?? false)
                return false;

            if (!_baseType.IsAssignableFrom(a_type))
                return false;

            if (!a_type.FullName.EndsWith(a_typeName + _suffix))
                return false;

            return true;
        }
    }
}