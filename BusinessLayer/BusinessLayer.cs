using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wags.DataAccess;
using Wags.DataModel;

namespace Wags.BusinessLayer
{
    public class BusinessLayer : IBusinessLayer
    {

    #region Repositiories
        private readonly IPlayerRepository _playerRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseDataRepository _courseDataRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ITrophyRepository _trophyRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IHistoryRepository _historyRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IScoreRepository _scoreRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IRoundRepository _roundRepository;       
    #endregion

        public BusinessLayer()
        {
            _playerRepository = new PlayerRepository();
            _memberRepository = new MemberRepository();
            _eventRepository = new EventRepository();
            _bookingRepository = new BookingRepository();
            _courseRepository = new CourseRepository();
            _historyRepository = new HistoryRepository();
        }

#region Members

        public IList<Member> GetAllMembers()
        {
            return _memberRepository.GetAll();
        }

        public IList<Member> GetAllCurrentMembers()
        {
            var nav = new Expression<Func<Member, object>>[]
            {
                d => d.Histories
            };
            return _memberRepository.GetList(
                 d => d.Histories.OrderByDescending(h => h.Date).FirstOrDefault().Status == PlayerStatus.Member,
                 nav);
        }

        public Member GetMemberById(int id)
        {
            return GetMember(d => d.Id == id);
        }

        public Member GetMemberByName(string name)
        {
            var names = name.Split(' ');
            var firstName = names[0];
            var lastName = names[1];

            return GetMember(d => d.FirstName == firstName && d.LastName == lastName);
       }

        private Member GetMemberAll(Expression<Func<Member, bool>> where)
        {
            var nav = new Expression<Func<Member, object>>[]
            {
                d => d.Histories,
                d => d.Scores,
                d => d.Transactions,
                d => d.Bookings
            };
            return _memberRepository.GetSingle(where, nav);
        }

        private Member GetMemberSimple(Expression<Func<Member, bool>> where)
        {
            return _memberRepository.GetSingle(where);
        }

        private Member GetMember(Expression<Func<Member, bool>> where)
        {
            return _memberRepository.GetSingle(where);
        }

        public IList<History> GetMemberHistory(int id)
        {
            var nav = new Expression<Func<Member, object>>[]
            {
                d => d.Histories
            };
            return _memberRepository.GetSingle(m => m.Id == id, nav).Histories.ToList();
        }

        public History GetMemberCurrentStatus(int id)
        {
            var nav = new Expression<Func<Member, object>>[]
            {
                d => d.Histories
            };
            return _memberRepository.GetSingle(m => m.Id == id, nav).CurrentStatus;
        }

        public void AddMember(Member member)
        {
            _memberRepository.Add(member);
        }

        public void UpdateMember(Member member)
        {
            _memberRepository.Update(member);
        }

        public void DeleteMember(int id)
        {
            var nav = new Expression<Func<Member, object>>[]
            {
                d => d.Histories,
                d => d.Scores,
                d => d.Transactions,
                d => d.Bookings
            };
            var member = _memberRepository.GetSingle(m => m.Id == id, nav);
            member.EntityState = EntityState.Deleted;
            foreach (var hist in member.Histories)
                hist.EntityState = EntityState.Deleted;
            //etc.
            _memberRepository.Remove(member);
        }

#endregion        
        
#region Players
		public IList<Player> GetAllPlayers()
        {
            return _playerRepository.GetAll();
        }

        public IList<History> GetPlayerHistory(int id)
        {
            return _historyRepository.GetList(d => d.Player.Id == id);
        }

        public History GetPlayerCurrentStatus(int id)
        {
            return _playerRepository.GetSingle(p => p.Id == id, p => p.Histories).CurrentStatus;
        }

        public History GetPlayerStatusAtDate(int id, DateTime date)
        {
            return _playerRepository.GetSingle(p => p.Id == id, p => p.Histories).StatusAtDate(date);
        }

#endregion
 
#region Events

        public IList<Event> GetAllEvents()
        {
            return _eventRepository.GetAll(d => d.Trophy);
        }

        public IList<Event> GetAllEvents(int year)
        {
            return _eventRepository.GetList(d => d.Date.Year == year, d => d.Trophy);
        }

        public Event GetEvent(int id)
        {
            var nav = new Expression<Func<Event, object>>[]
            {
                d => d.Trophy,
                d => d.Organisers,
                d => d.Bookings,
                d => d.Bookings.Select(b => b.Member),
                d => d.Bookings.Select(b => b.Guests)
            };  
            return _eventRepository.GetSingle(d => d.Id == id, nav);
        }

		public Event GetEventResult(int id)
        {
            var nav = new Expression<Func<Event, object>>[]
            {
                d => d.Rounds,
                d => d.Rounds.Select(r => r.Course),
                d => d.Rounds.Select(r => r.Scores),
                d => d.Rounds.Select(r => r.Scores.Select(s => s.Player)),
                d => d.Rounds.Select(r => r.Scores.Select(s => s.Player).Select(p => p.Histories))
            };  
            return _eventRepository.GetSingle(d => d.Id == id, nav);
        }

#endregion

#region Bookings

        public IList<Booking> GetBookingsForEvent(int eventId)
        {
            var nav = new Expression<Func<Booking, object>>[]
            {
                d => d.Event,
                d => d.Member,
                d => d.Guests
            };
            return _bookingRepository.GetList(d => (d.Event.Id == eventId), nav);
        }

        public Booking GetBooking(int id)
        {
            var nav = new Expression<Func<Booking, object>>[]
            {
                d => d.Member,
                d => d.Event,
                d => d.Guests
            };
            return _bookingRepository.GetSingle(d => d.Id == id, nav);
        }

        public Booking GetBookingForEventAndMember(int eventId, int memberId)
        {
            var nav = new Expression<Func<Booking, object>>[]
            {
                d => d.Event,
                d => d.Member,
                d => d.Guests
            };
            return _bookingRepository.GetSingle(d => (d.Event.Id == eventId) && (d.Member.Id == memberId), nav);
        }

        public int AddBooking(Booking booking)
        {
            booking.Timestamp = DateTime.Now;
            booking.EntityState = EntityState.Added;
            foreach (var guest in booking.Guests)
                guest.EntityState = EntityState.Added;
            _bookingRepository.Add(booking);
            return booking.Id;
        }

        public int UpdateBooking(Booking booking)
        {
            booking.Timestamp = DateTime.Now;
            booking.EntityState = EntityState.Modified;
            _bookingRepository.Update(booking);
            return booking.Id;
        }

        public void DeleteBooking(int id)
        {
            var booking = GetBooking(id);
            booking.EntityState = EntityState.Deleted;
            foreach (var guest in booking.Guests)
                guest.EntityState = EntityState.Deleted;
            _bookingRepository.Remove(booking);
        }
    }
    
#endregion
    //<Course>
    //<CourseData> 
    //<Trophy>

    //<Guest>
    //<History>
    //<Transaction>
    //<Score>
    //<Member>
    //<Club> 
    //<Round>
}
