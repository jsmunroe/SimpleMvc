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
        /// Get a type with the given type name (<paramref name="a_typeName"/>).
        /// </summary>
        /// <param name="a_typeName">Type name.</param>
        public object Resolve(string a_typeName)
        {
            #region Argument Validation

            if (a_typeName == null)
                throw new ArgumentNullException(nameof(a_typeName));

            #endregion

            if (!_typeTypesByName.ContainsKey(a_typeName))
                return null;

            var typeType = _typeTypesByName[a_typeName];
            var type = _container.Resolve(typeType);

            TypeNames.Add(a_typeName);

            return type;
        }

        /// <summary>
        /// Get a type with the given type name (<paramref name="a_typeName"/>).
        /// </summary>
        /// <param name="a_sub">Subdirectory name.</param>
        /// <param name="a_typeName">Type name.</param>
        /// <returns></returns>
        public object Resolve(string a_sub, string a_typeName)
        {
            throw new NotImplementedException();
        }
    }
}
