namespace SimpleMvc.Contracts
{
    public interface ITypeCatalog
    {
        /// <summary>
        /// Get a type with the given type name (<paramref name="a_typeName"/>).
        /// </summary>
        /// <param name="a_typeName">Type name.</param>
        object Resolve(string a_typeName);

        /// <summary>
        /// Get a type with the given type name () in the given namespace subbranch ().
        /// </summary>
        /// <param name="a_sub">Namespace subbranch.</param>
        /// <param name="a_typeName"></param>
        /// <returns></returns>
        object Resolve(string a_sub, string a_typeName);
    }
}