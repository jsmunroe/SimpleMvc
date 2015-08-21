using SimpleIoc;
using SimpleIoc.Contracts;
using SimpleMvc.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMvc.Handlers;
using SimpleMvc.Results;

namespace SimpleMvc.Test
{
    public class TestViewHandler : ViewHandler
    {
        public TestViewCatalog ViewCatalog { get { return _viewCatalog as TestViewCatalog; } }

        public KeyValuePair<string, object> LastResolvedView { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public TestViewHandler()
        {
            RegisterViewCatalog(new TestViewCatalog());
        }

        /// <summary>
        /// Handle the given result (<paramref name="a_result"/>).
        /// </summary>
        /// <param name="a_result">Result to handle.</param>
        public override void Handle(ViewResult a_result)
        {
            LastResolvedView = new KeyValuePair<string, object>(a_result.ViewName, a_result.Model);

            base.Handle(a_result);
        }
    }

}
