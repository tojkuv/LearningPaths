namespace UnitTestingExampleTests
{
    [TestClass]
    public class MSTestUnitTests
    {
        private const string Expected = "Hello World!";
                        
        [TestMethod]
        public void MSTestMethod1()
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