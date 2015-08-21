using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Test
{
    public class TestModel { }

    public class TestViewBase
    {
        public object DataModel { get; set; }
    }

    public class TestView1 : TestViewBase { }

    public class TestView2 : TestViewBase { }
}
