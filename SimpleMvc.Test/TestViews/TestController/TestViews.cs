using SimpleMvc.Contracts;

namespace SimpleMvc.Test.TestViews.TestController
{
    public class TestModel { }

    public class TestViewBase
    {
        public object DataModel { get; set; }
    }

    public class TestView1 : TestViewBase { }

    public class TestView2 : TestViewBase { }

    public class Index : TestViewBase { }

    public class EditUser : TestViewBase { }


    public class TestMvcModel : IMvcViewModel
    {
        public bool CleanupCalled { get; private set; } = false;

        public INavigator Navigator { get; set; }

        public string ControllerName { get; set; }

        /// <summary>
        /// Cleanup this view model. 
        /// </summary>
        public void Cleanup()
        {
            CleanupCalled = true;
        }
    }
}
