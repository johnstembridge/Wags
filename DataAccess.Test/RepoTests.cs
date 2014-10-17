using System;
using System.Linq.Expressions;
using Wags.DataModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Wags.DataAccess.Test
{
    [TestClass]
    public class RepoTests
    {
        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void GetMembers()
        {
            var db = new GenericDataRepository<Member>();
            var members = db.GetAll();
        }

        [TestMethod]
        public void GetOneMember()
        {
            var db = new GenericDataRepository<Member>();
            var member = db.GetSingle(d => d.Id == 1);
            Assert.IsNotNull(member);
       }

        [TestMethod]
        public void GetNonExistentMember()
        {
            var db = new GenericDataRepository<Member>();
            var member = db.GetSingle(d => d.Id == 9999);
            Assert.IsNull(member);
       }

        [TestMethod]
        public void GetOneMemberWithOneNavProperty()
        {
            var db = new GenericDataRepository<Member>();
            var member = db.GetSingle(d => d.Id == 1, d => d.Player);
        }

        [TestMethod]
        public void GetOneMemberWithNestedNavProperty()
        {
            var db = new GenericDataRepository<Member>();
            var member = db.GetSingle(d => d.Id == 1, d => d.Player.Histories);
            Assert.IsNotNull(member.Player.Histories);
        }

        [TestMethod]
        public void GetOneMemberWithMultipleNavProperties()
        {
            var db = new GenericDataRepository<Member>();
            var nav = new Expression<Func<Member, object>>[]
            {
                d => d.Player,
                d => d.Transactions,
                d => d.Bookings
            };
            var member = db.GetSingle(d => d.Id == 1, nav);
        }

        [TestMethod]
        public void GetOneMemberWithMultipleNestedNavProperties()
        {
            var db = new GenericDataRepository<Member>();
            var nav = new Expression<Func<Member, object>>[]
            {
                d => d.Player.Histories,
                d => d.Player.Scores,
                d => d.Transactions,
                d => d.Bookings
            };
            var member = db.GetSingle(d => d.Id == 1, nav);
        }

        [TestMethod]
        public void GetAMemberPlayerByName()
        {
            var db = new GenericDataRepository<Player>();
            var fName = "Peter";
            var lName = "Berring";
            var player = db.GetSingle(d => d.FirstName == fName && d.LastName == lName, d => d.Member);
            Assert.IsNotNull(player.Member);
        }

        [TestMethod]
        public void GetANonMemberPlayerByName()
        {
            var db = new GenericDataRepository<Player>();
            var fName = "Tom";
            var lName = "Gavin";
            var player = db.GetSingle(d => d.FirstName == fName && d.LastName == lName, d => d.Member);
            Assert.IsNull(player.Member);
        }

        [TestMethod]
        public void GetSelectionOfMembers()
        {
            var db = new GenericDataRepository<Member>();
            var wimbledonMembers = db.GetList(d => d.Address.PostCode.StartsWith("SW19"));
            Assert.IsTrue(wimbledonMembers.Count > 0);
        }
   }
}
