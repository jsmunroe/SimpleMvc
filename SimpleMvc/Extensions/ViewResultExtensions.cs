using SimpleMvc.Results;
using SimpleMvc.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace SimpleMvc.Extensions
{
    public static class ViewResultExtensions
    {
        public static bool TryGetViewNameFromModel(this ViewResult a_result, out string viewName)
        {
            if (!string.IsNullOrEmpty(a_result.ViewName))
            {
                viewName = a_result.ViewName;
                return true;
            }

            if (a_result.Model is not null)
            {
                var viewModelAttribute = a_result.Model.GetType().GetCustomAttribute<ViewModelAttribute>(true);

                if (viewModelAttribute is not null)
                {
                    viewName = viewModelAttribute.ActionName;
                    return true;
                }
            }

            viewName = null;
            return false;
        }
    }
}
