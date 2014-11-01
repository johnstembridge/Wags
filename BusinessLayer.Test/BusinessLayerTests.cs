using System;
using System.Linq;
using System.Collections.Generic;
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
        public void GetMember()
        {
            var member = bl.GetMemberById(12);
        }

        [TestMethod]
        public void GetMemberByName()
        {
            var member = bl.GetMemberByName("Tony Batt");
        }

        [TestMethod]
        public void GetMemberHistory()
        {
            var history = bl.GetMemberHistory(12);
        }

        [TestMethod]
        public void GetMemberStatus()
        {
            var current = bl.GetMemberCurrentStatus(12); // that's me
            Assert.AreEqual(PlayerStatus.Member, current.Status);
        }

        [TestMethod]
        public void UpdateMember()
        {
            var member = bl.GetMemberById(12);
            member.Phone = "07948 213164";
            member.EntityState = EntityState.Modified;
            bl.UpdateMember(member);
        }

        [TestMethod]
        [Ignore]
        public void AddMember()
        {
            var hist = new History()
            {
                Date = DateTime.Today,
                Status = PlayerStatus.Member,
                Handicap = 28,
                EntityState = EntityState.Added
            };
            var member = new Member()
            {
                FirstName = "Joe",
                LastName = "Blow",
                Phone = "07948 213164",
                Email = "joe.blow@gmail.com",
                Address = new Address(){StreetAddress="1,The Road, The Town", PostCode="SW19 8XX"},
                Histories = new List<History>(){hist}
            };
            member.EntityState = EntityState.Added;
            bl.AddMember(member);
            var newId = member.Id;
        }
        [TestMethod]
        [Ignore]
        public void RemoveMember()
        {
            bl.DeleteMember(375);
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
 
#region Event

        [TestMethod]
        public void GetAllEvents()
        {
            var events = bl.GetAllEvents();
        }
        [TestMethod]
        public void GetAllEventsForYear()
        {
            var events = bl.GetAllEvents(2014);
        }

        [TestMethod]
        public void GetSingleEvent()
        {
            var ev = bl.GetEvent(188);
        }

        [TestMethod]
        public void GetEventResult()
        {
            var res = bl.GetEventResult(186);
        }

        #endregion

#region Booking
		
        [TestMethod]
        public void GetBookingsForEvent()
        {
            var res = bl.GetBookingsForEvent(186);
        }
 
        [TestMethod]
        public void GetBookingForEventAndMember()
        {
            var res = bl.GetBookingForEventAndMember(186, 12);
        }

        [TestMethod]
        public void GetBooking()
        {
            var res = bl.GetBooking(240);
        }

        [TestMethod]
        public void CreateBooking()
        {
            var booking = new Booking()
            {
                MemberId = 12,
                EventId = 188,
                Attending = true,
                Comment = "can't make morning round"
            };
            var res = bl.AddBooking(booking);
        }

        [TestMethod]
        public void UpdateBooking()
        {
            var booking = bl.GetBookingForEventAndMember(188, 12);
            var guest = new Guest() 
            {
                Name = "Joe Blow",
                Handicap = 22,
                EntityState=EntityState.Added
            };
            booking.Guests.Add(guest);
            var res = bl.UpdateBooking(booking);
        }

        [TestMethod]
        public void RemoveBooking()
        {
            bl.DeleteBooking(279);
        }

#endregion

        
    
    }
}
