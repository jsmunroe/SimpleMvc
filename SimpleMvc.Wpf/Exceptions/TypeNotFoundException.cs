using System;

namespace SimpleMvc.Exceptions
{
    public class TypeNotFoundException : Exception
    {
        public TypeNotFoundException(string a_typeName)
            : base($"Cannot resolve a type for the given view name '{a_typeName}'.")
        {
            
        }
    }
}