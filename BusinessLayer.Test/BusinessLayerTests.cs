using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wags.DataModel;

namespace Wags.BusinessLayer.Test
{
    [TestClass]
    public class BusinessLayerTests
    {
        private BusinessLayer bl;
        private TransactionScope transaction;

        [TestInitialize]
        public void Init()
        {
            bl = new BusinessLayer();
            transaction = new System.Transactions.TransactionScope();
        }

        [TestCleanup]
        public void Final()
        {
            transaction.Dispose();
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
        public void AddMember()
        {
            var member = NewMember();
            Assert.AreEqual(0, member.Id);
            var newId = bl.AddMember(member);
            Assert.AreNotEqual(0, newId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveNonExistingMember()
        {
            bl.DeleteMember(0);
        }

        private static Member NewMember()
        {
            var hist = new History
            {
                Date = DateTime.Today,
                Status = PlayerStatus.Member,
                Handicap = 28,
                EntityState = EntityState.Added
            }; 
            
            var member = new Member
            {
                FirstName = "Joe",
                LastName = "Blow",
                Phone = "07948 213164",
                Email = "joe.blow@gmail.com",
                Address = new Address() {StreetAddress = "1,The Road, The Town", PostCode = "SW19 8XX"},
                Histories = new List<History>() {hist},
                EntityState = EntityState.Added
            };
            return member;
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
            var ev = bl.GetEventDetails(188);
        }

        [TestMethod]
        public void GetEventResult()
        {
            var res = bl.GetEventResult(186);
        }

        [TestMethod]
        public void CreateEvent()
        {
            var res = bl.AddEvent(NewEvent());
        }

        Event NewEvent()
        {
            var round = new Round {CourseId = 87, Date = new DateTime(2015, 3, 30)};
            var organiser = new Member {Id = 178};
            var ev = new Event()
            {
                Date = new DateTime(2015, 3, 30),
                Name = "West Byfleet",
                MemberPrice = 45.00M,
                GuestPrice = 55.00M,
                BookingDeadline = new DateTime(2015, 3, 26),
                MaxPlayers = 40,
                Schedule = "08.30 Full English Breakfast,09.30 Tee off 9 holes, 12.00 Ploughmans lunch, 13.00 Tee off 18 holes, 18.00 Prizes and drinks",
                Notes = "No guests for time being",
                Rounds = new [] {round},
                Trophy = new Trophy {Id = 1},
                Organisers = new []{organiser}
            };
            return ev;            
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
            var newId = bl.AddBooking(NewBooking());
        }

        [TestMethod]
        public void UpdateBooking()
        {
            var newId = bl.AddBooking(NewBooking());
            var booking = bl.GetBooking(newId);
            booking.Comment = "updated comment 1";
            booking.EntityState=EntityState.Modified;
            var res = bl.UpdateBooking(booking);
        }

        [TestMethod]
        public void UpdateBookingWithNewGuest()
        {
            var newId = bl.AddBooking(NewBooking());
            var booking = bl.GetBooking(newId);
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
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveNonExistingBooking()
        {
            bl.DeleteBooking(0);
        }

        static Booking NewBooking()
        {
            var booking = new Booking()
            {
                MemberId = 12,
                EventId = 188,
                Attending = true,
                Comment = "can't make morning round"
            };
            return booking;
        }

#endregion
         
    }
}
