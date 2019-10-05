using System;
using Xunit;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void givenTrue_assertTrue()
        {
            Assert.True(true);
        }
    }
}