using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Exceptions
{
    public class NavigationException : Exception
    {
        public NavigationException(string a_message)
            : base(a_message)
        {

        }

        public NavigationException(string a_message, Exception a_innerException)
            : base(a_message, a_innerException)
        {

        }
    }
}
