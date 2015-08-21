using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc
{
    public class RouteDictionary : Dictionary<string, object>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RouteDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
            
        }
    }
}
