using System;
using SimpleMvc.Contracts;

namespace SimpleMvc
{
    public abstract class ViewTarget<TView> : IViewTarget
    {
        /// <summary>
        /// Set this target to the given view (<paramref name="a_view"/>).
        /// </summary>
        /// <param name="a_view">View object.</param>
        public abstract void SetView(TView a_view);

        /// <summary>
        /// Set this target to the given view (<paramref name="a_view"/>).
        /// </summary>
        /// <param name="a_view">View object.</param>
        void IViewTarget.SetView(object a_view)
        {
            var view = a_view;

            if (view is TView == false)
                throw new InvalidOperationException($"This view target does not support this type of view ('{a_view.GetType().FullName}').");

            SetView((TView)a_view);
        }
    }
}