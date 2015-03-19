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
            _roundRepository = new RoundRepository();
            _historyRepository = new HistoryRepository();
        }

#region Members

        public IList<Member> GetAllMembers(bool current=true)
        {
            return current? GetAllCurrentMembers() : _memberRepository.GetAll();
        }

        IList<Member> GetAllCurrentMembers()
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

        public Member GetMember(int id)
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

        public Member GetMemberAllInfo(int id)
        {
            var nav = new Expression<Func<Member, object>>[]
            {
                d => d.Histories,
                d => d.Scores,
                d => d.Transactions,
                d => d.Bookings
            };
            return _memberRepository.GetSingle(d => d.Id == id, nav);
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
            if (!EventExists(eventId))
                return null;
            var nav = new Expression<Func<Booking, object>>[]
            {
                d => d.Member,
                d => d.Member.Histories,
                d => d.Guests
            };
            var bookings = _bookingRepository.GetList(d => (d.Event.Id == eventId), nav);
            var members = bookings.Select(b => b.Member.ToPlayer());
            var guests = (bookings.Count > 0) 
                ?bookings.Select(b => b.Guests.ToArray()).Aggregate((x, y) => x.Concat(y).ToArray()).Select(GuestToPlayer)
                :new List< Player>();
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

        public IList<Event> GetAllEvents()
        {
            return _eventRepository.GetAll(d => d.Trophy).OrderBy(d => d.Date).ToList();
        }

        public IList<Event> GetAllEvents(int year)
        {
            if (year == 0) return GetAllEvents();
            return _eventRepository.GetList(d => d.Date.Year == year, d => d.Trophy).OrderBy(d => d.Date).ToList();
        }

        public bool EventExists(int id)
        {
            return null != GetEvent(id);
        }

        public Event GetEventDetails(int id)
        {
            var nav = new Expression<Func<Event, object>>[]
            {
                d => d.Trophy,
                d => d.Organisers,
                d => d.Rounds,
                d => d.Rounds.Select(c => c.Course.CourseData),
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

		public Event GetEventResult(int id)
		{
		    if (!EventExists(id))
		        return null;
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
            try
            {
                _eventRepository.Add(newEvent);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.ToString().Contains("duplicate"))
                    newEvent = null;
                else
                    throw;
            }
            return newEvent;
      }

        public void UpdateEvent(Event eventObj)
        {
            _eventRepository.Update(eventObj);
        }

        public void DeleteEvent(int id)
        {
            var ev = GetEventAll(id);
            if (ev == null)
                throw new ArgumentException(string.Format("Event {0} not found", id));
            ev.EntityState = EntityState.Deleted;
            foreach (var booking in ev.Bookings)
                booking.EntityState = EntityState.Deleted;
            foreach (var round in ev.Rounds)
                round.EntityState = EntityState.Deleted;

            _eventRepository.Remove(ev);
        }

#endregion

#region Bookings

        public IList<Booking> GetEventBookings(int eventId)
        {
            if (!EventExists(eventId))
                return null; 
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

        public void UpdateBooking(Booking booking)
        {
            booking.Timestamp = DateTime.Now;
            booking.EntityState = EntityState.Modified;
            _bookingRepository.Update(booking);
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
#endregion

#region Courses

        public IList<Course> GetAllCourses(string clubName)
        {
            if (clubName.Length==0)
                return _courseRepository.GetAll().ToList();
            else
                return GetCoursesForClub(clubName);
        }

        public Course GetCourseDetails(int id)
        {
            var nav = new Expression<Func<Course, object>>[]
            {
                d => d.Club,
                d => d.CourseData
           };
            return _courseRepository.GetSingle(d => d.Id == id, nav);
        }

        public Course GetCourseAll(int id)
        {
            var nav = new Expression<Func<Course, object>>[]
            {
                d => d.Club,
                d => d.CourseData
            };  
            return _courseRepository.GetSingle(d => d.Id == id, nav);
        }

        public Course GetCourse(int id)
        {
            var nav = new Expression<Func<Course, object>>[] { }; 
            return _courseRepository.GetSingle(d => d.Id == id, nav);
        }

        public IList<Round> GetCourseRounds(int courseId)
        {
            var nav = new Expression<Func<Round, object>>[] { }; 
            return _roundRepository.GetList(d => d.CourseId == courseId, nav);
        }

       public IList<Course> GetCoursesForClub(string name)
        {
            var nav = new Expression<Func<Course, object>>[]
            {
                d => d.Club,
                d => d.CourseData
           };            return _courseRepository.GetList(d => d.Club.Name == name, nav);
        }

        public Course AddCourse(Course newCourse)
        {
            newCourse.EntityState = EntityState.Added;
            foreach (var courseData in newCourse.CourseData)
                courseData.EntityState = EntityState.Added;
            try
            {
                _courseRepository.Add(newCourse);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.ToString().Contains("duplicate"))
                    newCourse = null;
                else
                    throw;
            }
            return newCourse;
      }

        public void UpdateCourse(Course course)
        {
            _courseRepository.Update(course);
        }

        public void DeleteCourse(int id)
        {
            var c = GetCourseAll(id);
            if (c == null)
                throw new ArgumentException(string.Format("Course {0} not found", id));
            c.EntityState = EntityState.Deleted;
            foreach (var courseData in c.CourseData)
                courseData.EntityState = EntityState.Deleted;
            foreach (var round in c.Rounds)
                round.EntityState = EntityState.Deleted;

            _courseRepository.Remove(c);
        }

#endregion

    //<CourseData> 
    //<Trophy>

    //<Guest>
    //<History>
    //<Transaction>
    //<Score>
    //<Club> 
    //<Round>

    }

}
