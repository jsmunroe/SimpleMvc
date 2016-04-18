using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Contracts
{
    public interface IModelBinder
    {
        /// <summary>
        /// Bind the given model (<paramref name="a_model"/>) to the given view (<paramref name="a_view"/>).
        /// </summary>
        /// <param name="a_view">View.</param>
        /// <param name="a_model">Model</param>
        void Bind(object a_view, object a_model);

        /// <summary>
        /// Get the model currently bound to the given view (<paramref name="a_view"/>), null if no model is bound.
        /// </summary>
        /// <param name="a_view">View.</param>
        /// <returns>Model that is bound to the view.</returns>
        object GetModel(object a_view);
    }
}
