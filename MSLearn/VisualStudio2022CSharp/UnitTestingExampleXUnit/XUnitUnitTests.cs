namespace UnitTestingExampleXUnit
{
    public class XUnitUnitTests
    {
        private const string Expected = "Hello World!";

        [Fact]
        public void Test1()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                UnitTestingExample.Program.Main();

                var result = sw.ToString().Trim();
                Assert.Equal(Expected, result);

            }
        }
    }
}