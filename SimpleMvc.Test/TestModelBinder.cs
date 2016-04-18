using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMvc.Test.TestViews;
using SimpleMvc.Test.TestViews.TestController;

namespace SimpleMvc.Test
{
    public class TestModelBinder : ModelBinderBase<TestViewBase>
    {
        /// <summary>
        /// Bind the given model (<paramref name="a_model"/>) to the given view (<paramref name="a_view"/>).
        /// </summary>
        /// <param name="a_view">View.</param>
        /// <param name="a_model">Model</param>
        /// <exception cref="ArgumentNullException">Thrown if "<paramref name="a_view"/>" is null.</exception>
        public override void Bind(TestViewBase a_view, object a_model)
        {
            #region Argument Validation

            if (a_view == null)
                throw new ArgumentNullException(nameof(a_view));

            #endregion

            a_view.DataModel = a_model;
        }

        /// <summary>
        /// Get the model currently bound to the given view (<paramref name="a_view"/>), null if no model is bound.
        /// </summary>
        /// <param name="a_view">View.</param>
        /// <returns>Model that is bound to the view.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="a_view"/> is null.</exception>
        public override object GetModel(TestViewBase a_view)
        {
            #region Argument Validation

            if (a_view == null)
                throw new ArgumentNullException(nameof(a_view));

            #endregion

            return a_view.DataModel;
        }
    }
}
