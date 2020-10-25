using System;
using Xunit;
using Shamer_4001;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var X = new TestImp();
            X.BuildRet();

            foreach (Vehicle a in X.retList)
            {
                a.print();
            }
            
        }
    }
}
