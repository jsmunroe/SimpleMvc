namespace SimpleMvc.Contracts
{
    public interface ITypeCatalog
    {
        /// <summary>
        /// Get a type with the given catalog type name (<paramref name="a_catalogName"/>).
        ///  NOTE: Catalog may use the type name itself.
        /// </summary>
        /// <param name="a_catalogName">Catalog type name.</param>
        object Resolve(string a_catalogName);

        /// <summary>
        /// Get the catalog name from the given type name (<paramref name="a_typeName"/>).
        /// </summary>
        /// <param name="a_typeName">Type name.</param>
        /// <returns>Catalog name.</returns>
        string ToCatalogName(string a_typeName);

        /// <summary>
        /// Get the type name from the given catalog name (<paramref name="a_catalogName"/>).
        /// </summary>
        /// <param name="a_catalogName">Catalog name.</param>
        /// <returns>Type name.</returns>
        string ToTypeName(string a_catalogName);
    }
}