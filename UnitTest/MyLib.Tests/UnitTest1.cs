using System;
using Xunit;

namespace MyLib.Tests
{
    //https://docs.microsoft.com/zh-cn/dotnet/core/testing/unit-testing-with-dotnet-test
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var primeService = new PrimeService();
            bool result = primeService.IsPrime(1);
            Assert.False(result, "1 should not be prime");
        }

        [Fact]
        public void Test2()
        {
            var primeService = new PrimeService();
            bool result = primeService.IsPrime(1);
            Assert.True(result, "1 should not be prime");
        }
    }
}
