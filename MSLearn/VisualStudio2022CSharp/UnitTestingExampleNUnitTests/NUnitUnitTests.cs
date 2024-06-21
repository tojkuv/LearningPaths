namespace UnitTestingExampleNUnitTests
{
    public class Tests
    {
        private const string Expected = "Hello World!";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NUnitTestMethod1()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                UnitTestingExample.Program.Main();

                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);

            }
        }
    }
}