namespace SimpleMvc.Test.TestViews.TestController
{
    public class TestModel { }

    public class TestViewBase
    {
        public object DataModel { get; set; }
    }

    public class TestView1 : TestViewBase { }

    public class TestView2 : TestViewBase { }
}
