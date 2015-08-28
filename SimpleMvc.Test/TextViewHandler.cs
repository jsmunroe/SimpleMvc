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
        public TestTypeCatalog TypeCatalog => _viewCatalog as TestTypeCatalog;

        public KeyValuePair<string, object> LastResolvedView { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public TestViewHandler()
        {
            RegisterViewCatalog(new TestTypeCatalog());
        }

        /// <summary>
        /// Handle the given result (<paramref name="a_result"/>).
        /// </summary>
        /// <param name="a_mvc"></param>
        /// <param name="a_controllerName"></param>
        /// <param name="a_result">Result to handle.</param>
        public override void Handle(MvcEngine a_mvc, string a_controllerName, ViewResult a_result)
        {
            LastResolvedView = new KeyValuePair<string, object>(a_result.ViewName, a_result.Model);

            base.Handle(a_mvc, a_controllerName, a_result);
        }
    }

}
