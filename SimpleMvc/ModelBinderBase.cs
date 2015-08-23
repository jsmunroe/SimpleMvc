using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMvc.Contracts;

namespace SimpleMvc
{
    public abstract class ModelBinderBase<TView> : IModelBinder
    {
        /// <summary>
        /// Bind the given model (<paramref name="a_model"/>) to the given view (<paramref name="a_view"/>).
        /// </summary>
        /// <param name="a_view">View.</param>
        /// <param name="a_model">Model</param>
        public abstract void Bind(TView a_view, object a_model);

        /// <summary>
        /// Bind the given model (<paramref name="a_model"/>) to the given view (<paramref name="a_view"/>).
        /// </summary>
        /// <param name="a_view">View.</param>
        /// <param name="a_model">Model</param>
        void IModelBinder.Bind(object a_view, object a_model)
        {
            if (a_view is TView == false)
                return;

            Bind((TView)a_view, a_model);
        }
    }
}
