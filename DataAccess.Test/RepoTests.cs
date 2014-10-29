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
            var member = db.GetSingle(d => d.Id == 1, d => d.Histories);
        }

        [TestMethod]
        public void GetOneMemberWithMultipleNavProperties()
        {
            var db = new GenericDataRepository<Member>();
            var nav = new Expression<Func<Member, object>>[]
            {
                d => d.Histories,
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
                d => d.Histories,
                d => d.Scores,
                d => d.Transactions,
                d => d.Bookings
            };
            var member = db.GetSingle(d => d.Id == 1, nav);
        }

        [TestMethod]
        public void GetAMemberByName()
        {
            var db = new GenericDataRepository<Player>();
            var fName = "Peter";
            var lName = "Berring";
            var player = db.GetSingle(d => d.FirstName == fName && d.LastName == lName);
            Assert.IsNotNull(player);
        }

        [TestMethod]
        public void GetSelectionOfMembers()
        {
            var db = new GenericDataRepository<Member>();
            var wimbledonMembers = db.GetList(d => d.Address.PostCode.StartsWith("SW19"));
            Assert.IsTrue(wimbledonMembers.Count > 0);
        }

        [TestMethod]
        public void GetAllPlayers()
        {
            var db = new GenericDataRepository<Player>();
            var players = db.GetAll();
            Assert.IsNotNull(players);
        }

    }
}
