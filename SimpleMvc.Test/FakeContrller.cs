using System;

namespace SimpleMvc.Test
{
    public class FakeContrller : ControllerBase
    {
        public bool IndexCalled { get; private set; } = false;

        public ActionResult Index()
        {
            IndexCalled = true;
            return null;
        }
    }
}
