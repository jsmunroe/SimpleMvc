namespace SimpleMvc.Contracts
{
    public interface IViewCatalog
    {
        /// <summary>
        /// Get a view with the given view name (<paramref name="a_viewName"/>).
        /// </summary>
        /// <param name="a_viewName">View name.</param>
        /// <param name="a_model">Model to apply to the view.</param>
        object GetView(string a_viewName, object a_model);
    }
}