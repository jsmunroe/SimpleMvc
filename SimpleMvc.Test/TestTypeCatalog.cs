using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIoc;
using SimpleMvc.Contracts;

namespace SimpleMvc.Test
{
    public class TestTypeCatalog : ITypeCatalog
    {
        private readonly Container _container = new Container();
        private readonly Dictionary<string, Type> _typeTypesByName = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        public List<string> TypeNames { get; private set; } = new List<string>();

        /// <summary>
        /// Register the given type of type (<typeparamref name="TType"/>) under the given type name (<paramref name="a_typeName"/>).
        /// </summary>
        /// <typeparam name="TType">Type of type.</typeparam>
        /// <param name="a_typeName">Type name.</param>
        public void RegisterType<TType>(string a_typeName)
        {
            _typeTypesByName[a_typeName] = typeof (TType);
        }

        /// <summary>
        /// Get a type with the given type name (<paramref name="a_catalogName"/>).
        /// </summary>
        /// <param name="a_catalogName">Type name.</param>
        public object Resolve(string a_catalogName)
        {
            #region Argument Validation

            if (a_catalogName == null)
                throw new ArgumentNullException(nameof(a_catalogName));

            #endregion

            if (!_typeTypesByName.ContainsKey(a_catalogName))
                return null;

            var typeType = _typeTypesByName[a_catalogName];
            var type = _container.Resolve(typeType);

            TypeNames.Add(a_catalogName);

            return type;
        }

        /// <summary>
        /// Get the catalog name from the given type name (<paramref name="a_typeName"/>).
        /// </summary>
        /// <param name="a_typeName">Type name.</param>
        /// <returns>Catalog name.</returns>
        public string ToCatalogName(string a_typeName)
        {
            return a_typeName;
        }

        /// <summary>
        /// Get the type name from the given catalog name (<paramref name="a_catalogName"/>).
        /// </summary>
        /// <param name="a_catalogName">Catalog name.</param>
        /// <returns>Type name.</returns>
        public string ToTypeName(string a_catalogName)
        {
            return a_catalogName;
        }
    }
}
