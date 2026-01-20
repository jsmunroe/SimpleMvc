using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Results
{
    public class ViewResult : ActionResult
    {
        /// <summary>
        /// View name.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// View model.
        /// </summary>
        public object Model { get; set; }
    }
}
