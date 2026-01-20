using System;
using SimpleMvc.Contracts;
using SimpleMvc.Results;

namespace SimpleMvc.Handlers
{
    public abstract class ResultHandlerBase<TResult> : IResultHandler
        where TResult : ActionResult
    {
        /// <summary>
        /// Handle the given result (<paramref name="a_result"/>).
        /// </summary>
        /// <param name="a_mvc"></param>
        /// <param name="a_controllerName"></param>
        /// <param name="a_result">Result to handle.</param>
        public abstract void Handle(MvcEngine a_mvc, string a_controllerName, TResult a_result);

        /// <summary>
        /// Handle the given result (<paramref name="a_result"/>).
        /// </summary>
        /// <param name="a_mvc"></param>
        /// <param name="a_controllerName"></param>
        /// <param name="a_result">Result to handle.</param>
        void IResultHandler.Handle(MvcEngine a_mvc, string a_controllerName, ActionResult a_result)
        {
            var result = a_result as TResult;

            if (result == null)
                throw new InvalidOperationException($"This result handler does not handle this type of action result ('{a_result.GetType().FullName}').");

            Handle(a_mvc, a_controllerName, result);
        }
    }
}
