using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc
{
    /// <summary>
    /// Case-insensitive dictionary of named route values.
    /// </summary>
    public class RouteDictionary : Dictionary<string, object>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RouteDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="a_values">Source values.</param>
        public RouteDictionary(dynamic a_values)
            : this()
        {
            if (a_values != null)
            {
                foreach (var propertyDescriptor in TypeDescriptor.GetProperties(a_values))
                {
                    object obj = propertyDescriptor.GetValue(a_values);
                    Add(propertyDescriptor.Name, obj);
                }
            }
        }
    }
}
