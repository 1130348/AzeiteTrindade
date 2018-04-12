using LusiadasSolucaoWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LusiadasSolucaoWebTests.Controllers
{
    [TestClass()]
    public class ATPControllerTests
    {

        UserInfo uinfo = new UserInfo();

        [TestMethod()]
        public void IndexTest()
        {

            ATPModel atpTable = new ATPModel();

            // Assert
            Assert.IsNotNull(atpTable);
        }
    }
}