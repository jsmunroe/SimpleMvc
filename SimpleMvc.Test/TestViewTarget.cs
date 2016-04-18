using SimpleMvc.Contracts;

namespace SimpleMvc.Test
{
    public class TestViewTarget : IViewTarget
    {
        public object LastSetView { get; set; }

        /// <summary>
        /// Set the given view (<paramref name="a_view"/>) in this target.
        /// </summary>
        /// <param name="a_view">View.</param>
        public void SetView(object a_view)
        {
            LastSetView = a_view;
        }

        /// <summary>
        /// Get the view from this target.
        /// </summary>
        /// <returns>View.</returns>
        public object GetView()
        {
            return LastSetView;
        }
    }
}