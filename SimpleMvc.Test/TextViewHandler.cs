using SimpleIoc;
using SimpleIoc.Contracts;
using SimpleMvc.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMvc.Results;

namespace SimpleMvc.Test
{
    public class TestBootstrappableViewHandler: ResultHandlerBase<ViewResult>, IBootstrappable
    {
        public KeyValuePair<string, object> LastResolvedView { get; private set; }

        /// <summary>
        /// Whether this catalog is bootstrapped.
        /// </summary>
        public bool IsBootstrapped { get; private set; } = false;

        /// <summary>
        /// Bootstrap this catalog against the given container (<paramref name="a_container"/>).
        /// </summary>
        /// <param name="a_container">Container.</param>
        public void Bootstrap(Container a_container)
        {
            IsBootstrapped = true;
        }

        /// <summary>
        /// Handle the given result (<paramref name="a_result"/>).
        /// </summary>
        /// <param name="a_result">Result to handle.</param>
        public override void Handle(ViewResult a_result)
        {
            LastResolvedView = new KeyValuePair<string, object>(a_result.ViewName, a_result.Model);
        }
    }

    public class TestViewHandler : ResultHandlerBase<ViewResult>
    {
        public KeyValuePair<string, object> LastResolvedView { get; private set; }

        /// <summary>
        /// Handle the given result (<paramref name="a_result"/>).
        /// </summary>
        /// <param name="a_result">Result to handle.</param>
        public override void Handle(ViewResult a_result)
        {
            LastResolvedView = new KeyValuePair<string, object>(a_result.ViewName, a_result.Model);
        }
    }

}
