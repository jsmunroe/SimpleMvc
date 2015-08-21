using System;

namespace SimpleMvc.Exceptions
{
    public class ViewNotFoundException : Exception
    {
        public ViewNotFoundException(String a_viewName)
            : base($"Cannot resolve a view for the given view name '{a_viewName}'.")
        {
            
        }
    }
}