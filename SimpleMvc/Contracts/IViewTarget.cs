namespace SimpleMvc.Contracts
{
    public interface IViewTarget
    {
        /// <summary>
        /// Set the given view (<paramref name="a_view"/>) in this target.
        /// </summary>
        /// <param name="a_view">View.</param>
        void SetView(object a_view);

        /// <summary>
        /// Get the view from this target.
        /// </summary>
        /// <returns>View.</returns>
        object GetView();
    }
}