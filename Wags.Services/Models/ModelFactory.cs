using System.Linq;
using Wags.DataModel;

namespace Wags.Services.Models
{
    public class ModelFactory
    {
#region Event
        public EventModel Create(Event eventData)
        {
            return new EventModel()
            {
                Id = eventData.Id,
                Name = eventData.Name,
                Date = eventData.Date,
                IsOpen = eventData.IsOpen,
                MemberPrice = eventData.MemberPrice,
                GuestPrice = eventData.GuestPrice,
                DinnerPrice = eventData.DinnerPrice,
                BookingDeadline = eventData.BookingDeadline,
                MaxPlayers = eventData.MaxPlayers,
                Schedule = eventData.Schedule,
                Notes = eventData.Notes,
                Url = eventData.Url,
                Rounds = eventData.Rounds.Select(Create).ToList(),
                Trophy = (eventData.Trophy != null) ? Create(eventData.Trophy) : null,
                Organisers = eventData.Organisers.Select(Create).ToList(),
                EntityState = (EntityState)eventData.EntityState
            };
        }

        public Event Parse(EventModel eventData)
        {
            return new Event()
            {
                Id = eventData.Id,
                Name = eventData.Name,
                Date = eventData.Date,
                MemberPrice = eventData.MemberPrice,
                GuestPrice = eventData.GuestPrice,
                DinnerPrice = eventData.DinnerPrice,
                BookingDeadline = eventData.BookingDeadline,
                MaxPlayers = eventData.MaxPlayers,
                Schedule = eventData.Schedule,
                Notes = eventData.Notes,
                Url = eventData.Url,
                Rounds = eventData.Rounds.Select(Parse).ToList(),
                Trophy = Parse(eventData.Trophy),
                Organisers = eventData.Organisers.Select(Parse).ToList(),
                EntityState = (DataModel.EntityState)eventData.EntityState
            };
        }
        
#endregion
        
#region Round
        public RoundModel Create(Round roundData)
        {
            return new RoundModel()
            {
                Id = roundData.Id,
                Date = roundData.Date,
                Course = Create(roundData.Course),
                Scores = roundData.Scores.Select(Create).ToList()
           };
        }

        public Round Parse(RoundModel roundData)
        {
            return new Round()
            {
                Id = roundData.Id,
                Date = roundData.Date,
                Course = Parse(roundData.Course),
                Scores = roundData.Scores.Select(Parse).ToList()
            };
        }
        
#endregion

#region Course
		public CourseModel Create(Course courseData)
        {
            return new CourseModel()
            {
                Id = courseData.Id,
                Name = courseData.Name,
                Club = Create(courseData.Club),
                CourseData = courseData.CourseData.Select(Create).ToList()
            };
        }

        public Course Parse(CourseModel courseData)
        {
            if (courseData == null)
                return null;
            return new Course()
            {
                Id = courseData.Id,
                Name = courseData.Name,
                Club = Parse(courseData.Club)
            };
        }

#endregion

#region CourseData
        public CourseDataModel Create(CourseData courseData)
        {
            return new CourseDataModel()
            {
                Id = courseData.Id,
                EffectiveDate = courseData.EffectiveDate,
                SSS = courseData.SSS,
                Par = courseData.Par
            };
        }

        public CourseData Parse(CourseDataModel courseData)
        {
            return new CourseData()
            {
                Id = courseData.Id,
                EffectiveDate = courseData.EffectiveDate,
                SSS = courseData.SSS,
                Par = courseData.Par
            };
        }
#endregion

#region Club
        public ClubModel Create(Club clubData)
        {
            return new ClubModel()
            {
                Id = clubData.Id,
                Name = clubData.Name,
                Url = clubData.Url,
                Phone = clubData.Phone,
                Address = Create(clubData.Address),
                Directions = clubData.Directions
            };
        }

        public Club Parse(ClubModel clubData)
        {
            return new Club()
            {
                Id = clubData.Id,
                Name = clubData.Name,
                Url = clubData.Url,
                Phone = clubData.Phone,
                Address = Parse(clubData.Address),
                Directions = clubData.Directions
            };
        }      
#endregion

#region Trophy
		public TrophyModel Create(Trophy trophyData)
        {
            return new TrophyModel()
            {
                Id = trophyData.Id,
                Name = trophyData.Name
            };
        }

       public Trophy Parse(TrophyModel trophyData)
        {
            return new Trophy()
            {
                Id = trophyData.Id,
                Name = trophyData.Name
            };
        }
#endregion

#region Member
        public MemberModel Create(Member memberData)
        {
            return new MemberModel()
            {
                Id = memberData.Id,
                FirstName = memberData.FirstName,
                LastName = memberData.LastName,
                Email = memberData.Email,
                Phone = memberData.Phone,
                Address = Create(memberData.Address)
           };
        }

        public Member Parse(MemberModel memberData)
        {
            return new Member()
            {
                Id = memberData.Id,
                FirstName = memberData.FirstName,
                LastName = memberData.LastName,
                Email = memberData.Email,
                Phone = memberData.Phone,
                Address = Parse(memberData.Address)
           };
        }
#endregion

#region Address
		public Address Create(DataModel.Address memberData)
        {
            return new Address()
            {
                StreetAddress = memberData.StreetAddress,
                Country = memberData.Country,
                PostCode = memberData.PostCode
            };
        }

        public DataModel.Address Parse(Address memberData)
        {
            if (memberData == null)
                return null;
            return new DataModel.Address()
            {
                StreetAddress = memberData.StreetAddress,
                Country = memberData.Country,
                PostCode = memberData.PostCode
            };
        }
#endregion

#region Player
        public PlayerModel Create(Player playerData)
        {
            return new PlayerModel()
            {
                Id = playerData.Id,
                FirstName = playerData.FirstName,
                LastName = playerData.LastName,
                Handicap = playerData.CurrentStatus.Handicap,
                Status = Create(playerData.CurrentStatus)
            };
        }
 #endregion

#region Status

        public StatusModel Create(History statusData)
        {
            return new StatusModel()
            {
                Id = statusData.Id,
                Date = statusData.Date,
                Status = (PlayerStatus)statusData.Status,
                Handicap = statusData.Handicap
            };
        }
 #endregion

#region Guest
        public GuestModel Create(Guest guestData)
        {
            return new GuestModel()
            {
                Id = guestData.Id,
                Name = guestData.Name,
                Handicap = guestData.Handicap
           };
        }

        public Guest Parse(GuestModel guestData)
        {
            return new Guest()
            {
                Id = guestData.Id,
                Name = guestData.Name,
                Handicap = guestData.Handicap
           };
        }
#endregion

#region Booking
        public BookingModel Create(Booking bookingData)
        {
            return new BookingModel()
            {
                Id = bookingData.Id,
                EventId = bookingData.EventId,
                Timestamp = bookingData.Timestamp,
                Member = Create(bookingData.Member),
                Attending = bookingData.Attending,
                Comment = bookingData.Comment,
                Guests = bookingData.Guests.Select(Create).ToList()
            };
        }

        public Booking Parse(BookingModel bookingData)
        {
            return new Booking()
            {
                Id = bookingData.Id,
                EventId = bookingData.EventId,
                Timestamp = bookingData.Timestamp,
                Member = Parse(bookingData.Member),
                Attending = bookingData.Attending,
                Comment = bookingData.Comment,
                Guests = bookingData.Guests.Select(Parse).ToList()
            };
        }
#endregion
    
#region Score
        public ScoreModel Create(Score scoreData)
        {
            return new ScoreModel()
            {
                Id = scoreData.Id,
                PlayerId = scoreData.Player.Id,
                Player = scoreData.Player.FullName,
                Status = Create(scoreData.Player.StatusAtDate(scoreData.Round.Date)),
                Position = scoreData.Position,
                Points = scoreData.Points,
                Shots = scoreData.Shots
            };
        }

        public Score Parse(ScoreModel scoreData)
        {
            return new Score()
            {
                Id = scoreData.Id,
                PlayerId = scoreData.PlayerId,
                Position = scoreData.Position,
                Points = scoreData.Points,
                Shots = scoreData.Shots
            };
        }
#endregion
    
#region EventResult
        public EventResultModel CreateEventResult(Event eventData)
        {
            return new EventResultModel()
            {
                EventId = eventData.Id,
                Date = eventData.Date,
                Name = eventData.Name,
                Rounds = eventData.Rounds.Select(Create).ToList()
            };
        }

        public Event ParseEventResult(EventResultModel eventData)
        {
            return new Event()
            {
                Id = eventData.EventId,
                Date = eventData.Date,
                Name = eventData.Name,
                Rounds = eventData.Rounds.Select(Parse).ToList()
            };
        }
#endregion
    
    }
}