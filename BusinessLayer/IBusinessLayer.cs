using System;
using System.Collections.Generic;
using Wags.DataModel;

namespace Wags.BusinessLayer
{
    public interface IBusinessLayer
    {
        IList<Event> GetAllEvents();
        IList<Event> GetAllEvents(int year);
        Event GetEventDetails(int id);
        Event AddEvent(Event newEvent);
        void UpdateEvent(Event newEvent);
        void DeleteEvent(int id);

        IList<Booking> GetEventBookings(int eventId);
        Booking GetBooking(int id);
        Booking GetBookingForEventAndMember(int eventId, int memberId);
        Booking AddBooking(Booking newBooking);
        void UpdateBooking(Booking newBooking);
        void DeleteBooking(int id);

        IList<Member> GetAllMembers(bool current=true);
        Member GetMember(int id);
        Member GetMemberByName(string name);
        Member GetMemberAllInfo(int id);
        IList<History> GetMemberHistory(int id);
        History GetMemberCurrentStatus(int id);
        int AddMember(Member newMember);
        void UpdateMember(Member newMember);
        void DeleteMember(int id);


        IList<History> GetPlayerHistory(int id);
        History GetPlayerCurrentStatus(int id);
        History GetPlayerStatusAtDate(int id, DateTime date);
        IList<Player> GetPlayersForEvent(int eventId);
        //int AddPlayer(Player newPlayer);
        //void UpdatePlayer(Player newPlayer);
        //void DeletePlayer(int id);
    }
}
