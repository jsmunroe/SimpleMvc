namespace SimpleMvc.Results
{
    public class RediectResult : ActionResult
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Action name to which to redirect.
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Route values.
        /// </summary>
        public RouteDictionary Values { get; set; }
    }
}