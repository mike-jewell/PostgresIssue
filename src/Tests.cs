using Xunit;
using Xunit.Abstractions;

namespace Example
{
    public class Tests : BaseTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public Tests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void test_1()
        {
            var doesntMatter = true;
            Assert.True(doesntMatter);
        }

        [Fact]
        public void test_2()
        {
            var doesntMatter = true;
            Assert.True(doesntMatter);
        }
    }
}
