using NUnit.Framework;

namespace CartesianViewerModule.Tests
{

    [TestFixture]
    public class BaseUnitTest
    {
        protected BaseUnitTest()
        {
            //init plugins
            InitPlugin();
        }

        private static void InitPlugin()
        {
        }
    }
}