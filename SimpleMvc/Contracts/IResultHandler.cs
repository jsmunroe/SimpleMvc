using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMvc.Results;

namespace SimpleMvc.Contracts
{
    public interface IResultHandler
    {
        /// <summary>
        /// Handle the given result (<paramref name="a_result"/>).
        /// </summary>
        /// <param name="a_mvc">Mvc engine from which is the result.</param>
        /// <param name="a_controllerName"></param>
        /// <param name="a_result">Result to handle.</param>
        void Handle(MvcEngine a_mvc, string a_controllerName, ActionResult a_result);
    }
}
