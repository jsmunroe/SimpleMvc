using System;
using System.Windows;

namespace SimpleMvc.Wpf
{
    public class DataContextBinder : ModelBinderBase<FrameworkElement>
    {
        /// <summary>
        /// Bind the given model (<paramref name="a_model"/>) to the given view (<paramref name="a_view"/>).
        /// </summary>
        /// <param name="a_view">View.</param>
        /// <param name="a_model">Model</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_view"/> is null.</exception>
        public override void Bind(FrameworkElement a_view, object a_model)
        {
            #region Argument Validation

            if (a_view == null)
                throw new ArgumentNullException(nameof(a_view));

            #endregion

            a_view.DataContext = a_model;
        }

        /// <summary>
        /// Get the model currently bound to the given view (<paramref name="a_view"/>), null if no model is bound.
        /// </summary>
        /// <param name="a_view">View.</param>
        /// <returns>Model that is bound to the view.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_view"/> is null.</exception>
        public override object GetModel(FrameworkElement a_view)
        {
            #region Argument Validation

            if (a_view == null)
                throw new ArgumentNullException(nameof(a_view));

            #endregion
            
            return a_view.DataContext;
        }
    }
}