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

        public IEnumerable<Member> GetAllMembers(bool current)
        {
            return current? GetAllCurrentMembers() : _memberRepository.GetAll();
        }

        public IEnumerable<Member> GetAllCurrentMembers()
        {
            var nav = new Expression<Func<Member, object>>[] 
            {
                 d => d.Histories
            };
            var res = _memberRepository.GetList(
                 d => d.Histories.OrderByDescending(h => h.Date).FirstOrDefault().Status == PlayerStatus.Member,
                 nav);
            return res;
        }

        public Member GetMemberById(int id)
        {
            return _memberRepository.GetSingle(d => d.Id == id);
        }

        public Member GetMemberByName(string name)
        {
            var names = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var firstName = names[0].ToLower();
            var lastName = names[1].ToLower();
            return _memberRepository.GetSingle(d => d.FirstName.ToLower() == firstName && d.LastName.ToLower() == lastName);
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

        public IEnumerable<History> GetMemberHistory(int id)
        {
            var nav = new Expression<Func<Member, object>>[]
            {
                d => d.Histories
            };
            return _memberRepository.GetSingle(m => m.Id == id, nav).Histories;
        }

        public History GetMemberCurrentStatus(int id)
        {
            var nav = new Expression<Func<Member, object>>[]
            {
                d => d.Histories
            };
            return _memberRepository.GetSingle(m => m.Id == id, nav).CurrentStatus;
        }

        public int AddMember(Member member)
        {
            _memberRepository.Add(member);
            return member.Id;
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
            if (member == null)
                throw new ArgumentException(string.Format("Member {0} not found", id));
            member.EntityState = EntityState.Deleted;
            foreach (var hist in member.Histories)
                hist.EntityState = EntityState.Deleted;
            foreach (var score in member.Scores)
                score.EntityState = EntityState.Deleted;
            foreach (var transaction in member.Transactions)
                transaction.EntityState = EntityState.Deleted;
            foreach (var booking in member.Bookings)
                booking.EntityState = EntityState.Deleted;
            _memberRepository.Remove(member);
        }

#endregion        
        
#region Players
		public IList<Player> GetAllPlayers()
        {
            return _playerRepository.GetAll();
        }

        public Player GetPlayerByName(string name)
        {
            var names = name.Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var firstName = names[0].ToLower();
            var lastName = names[1].ToLower();
            var nav = new Expression<Func<Player, object>>[]
            {
                d => d.Scores,
                d => d.Histories,
            };
            return _playerRepository.GetSingle(p => p.FirstName.ToLower() == firstName && p.LastName.ToLower() == lastName, nav);
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

        public IList<Player> GetPlayersForEvent(int eventId)
        {
            var nav = new Expression<Func<Booking, object>>[]
            {
                d => d.Member,
                d => d.Member.Histories,
                d => d.Guests
            };
            var bookings = _bookingRepository.GetList(d => (d.Event.Id == eventId), nav);
            var members = bookings.Select(b => b.Member.ToPlayer());
            //var players = members.Select(m => m.ToPlayer());
            var guests = bookings.Select(b => b.Guests.ToArray()).Aggregate((x, y) => x.Concat(y).ToArray()).Select(GuestToPlayer);
            var players = members.Concat(guests).ToList();
            var eventDate = GetEvent(eventId).Date;
            foreach (Player p in players)
            {
                p.SetStatusAtDate(eventDate);
                p.Histories = null;
                p.Scores = null;
            }
            return players;
        }

        Player GuestToPlayer(Guest guest)
        {
            var player = GetPlayerByName(guest.Name);
            var status = new History() {Status = PlayerStatus.Guest, Handicap=guest.Handicap, Date=guest.Booking.Timestamp};
            if (player == null)
            {
                player = new Player()
                {
                    FirstName = guest.FirstName(),
                    LastName = guest.LastName(),
                    Histories = new List<History>{status}
                };
            }
            else if (player.CurrentStatus.Handicap != status.Handicap)
            {
                player.Histories.Add(status);
            }
            return player;
        }

#endregion
 
#region Events

        public IEnumerable<Event> GetAllEvents()
        {
            return _eventRepository.GetAll(d => d.Trophy).OrderBy(d => d.Date);
        }

        public IEnumerable<Event> GetAllEvents(int year)
        {
            if (year == 0) return GetAllEvents();
            return _eventRepository.GetList(d => d.Date.Year == year, d => d.Trophy).OrderBy(d => d.Date);
        }

        public Event GetEventDetails(int id)
        {
            var nav = new Expression<Func<Event, object>>[]
            {
                d => d.Trophy,
                d => d.Organisers,
                d => d.Rounds,
                d => d.Rounds.Select(c => c.Course.Club)
            };  
            return _eventRepository.GetSingle(d => d.Id == id, nav);
        }

        public Event GetEventAll(int id)
        {
            var nav = new Expression<Func<Event, object>>[]
            {
                d => d.Trophy,
                d => d.Organisers,
                d => d.Rounds,
                d => d.Bookings
            };  
            return _eventRepository.GetSingle(d => d.Id == id, nav);
        }

        public Event GetEvent(int id)
        {
            var nav = new Expression<Func<Event, object>>[] {}; 
            return _eventRepository.GetSingle(d => d.Id == id, nav);
        }

        public Event GetEventBookings(int id)
        {
            var nav = new Expression<Func<Event, object>>[]
            {
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

        public Event AddEvent(Event newEvent)
        {
            newEvent.EntityState = EntityState.Added;
            foreach (var round in newEvent.Rounds)
                round.EntityState = EntityState.Added;
            _eventRepository.Add(newEvent);
            return newEvent;
      }

        public int UpdateEvent(Event eventObj)
        {
            return 0;
        }

        public void DeleteEvent(int id)
        {
            var ev = GetEventAll(id);
            if (ev == null)
                throw new ArgumentException(string.Format("Event {0} not found", id));
            ev.EntityState = EntityState.Deleted;
            foreach (var round in ev.Rounds)
                round.EntityState = EntityState.Deleted;
            foreach (var booking in ev.Bookings)
                booking.EntityState = EntityState.Deleted;
            _eventRepository.Remove(ev);
        }

        #endregion

#region Bookings

        public IList<Booking> GetBookingsForEvent(int eventId)
        {
            var nav = new Expression<Func<Booking, object>>[]
            {
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
                d => d.Guests
            };
            return _bookingRepository.GetSingle(d => d.Id == id, nav);
        }

        public Booking GetBookingForEventAndMember(int eventId, int memberId)
        {
            var nav = new Expression<Func<Booking, object>>[]
            {
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
            if (booking == null)
                throw new ArgumentException(string.Format("Booking {0} not found", id));
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
    //<Club> 
    //<Round>
}
