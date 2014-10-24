using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wags.DataModel;

namespace Wags.BusinessLayer.Test
{
    [TestClass]
    public class BusinessLayerTests
    {
        private BusinessLayer bl;

        [TestInitialize]
        public void Init()
        {
            bl = new BusinessLayer();           
        }

#region Member
        [TestMethod]
        public void GetCurrentMembers()
        {
            var members = bl.GetAllCurrentMembers();
        }

        [TestMethod]
        public void GetMemberHistory()
        {
            var history = bl.GetMemberHistory(6);
        }

        [TestMethod]
        public void GetMemberStatus()
        {
            var current = bl.GetMemberCurrentStatus(6); // that's me
            Assert.AreEqual(PlayerStatus.Member, current.Status);
        }
        
        [TestMethod]
        public void UpdateMember()
        {
            var member = bl.GetMemberById(6);
            member.Phone = "07948 213164";
            member.EntityState = EntityState.Modified;
            member.Player.EntityState = EntityState.Modified;
            bl.UpdateMember(member);
        }

#endregion

#region Player
		
        [TestMethod]
        public void GetPlayerStatus()
        {
            var current = bl.GetPlayerCurrentStatus(12); // that's me
            Assert.AreEqual(PlayerStatus.Member, current.Status);
        }

        [TestMethod]
        public void GetPlayerHandicap()
        {
            var current = bl.GetPlayerCurrentStatus(12); // that's me
            Assert.AreEqual(28, current.Handicap);
        }

        [TestMethod]
        public void GetPlayerHandicapAtDate()
        {
            var current = bl.GetPlayerStatusAtDate(12, new DateTime(2005, 3, 25)); // that's me
            Assert.AreEqual(27, current.Handicap);
        }

#endregion 
    
    }
}
