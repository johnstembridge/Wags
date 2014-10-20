using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Wags.BusinessLayer.Test
{
    [TestClass]
    public class BusinessLayerTests
    {
        [TestMethod]
        public void GetMembers()
        {
            var bl = new BusinessLayer();
            var members = bl.GetAllMembers();
        }


    }
}
