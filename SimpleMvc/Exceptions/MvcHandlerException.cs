using System;

namespace SimpleMvc.Exceptions
{
    public class MvcHandlerException : Exception
    {
        public MvcHandlerException(string a_message)
            : base(a_message)
        {
        }
    }
}