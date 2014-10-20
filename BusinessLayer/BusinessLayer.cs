using System;
using System.Collections.Generic;
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
            _courseRepository = new CourseRepository();
        }

    #region Members

		public IList<Member> GetAllMembers()
        {
            return _memberRepository.GetAll(d => d.Player);
        }

        public Member GetMemberById(int id)
        {
            return GetMember(d => d.Id == id);
        }

        public Member GetMemberByName(string name)
        {
            var names = name.Split(' ');
            return GetMember(d => d.Player.FirstName == names[0] && d.Player.LastName == names[1]);
        }

        private Member GetMemberAll(Expression<Func<Member, bool>> where)
        {
            var nav = new Expression<Func<Member, object>>[]
            {
                d => d.Player,
                d => d.Player.Scores,
                d => d.Transactions,
                d => d.Bookings
            };
            return _memberRepository.GetSingle(where, nav);          
        }

        private Member GetMember(Expression<Func<Member, bool>> where)
        {
            return _memberRepository.GetSingle(where, d => d.Player);          
        }

	#endregion        
        
    #region Players
		public IList<Player> GetAllPlayers()
        {
            return _playerRepository.GetAll();
        }

	#endregion    
    }

    //<Player>
    //<Course>
    //<CourseData> 
    //<Event>
    //<Trophy>
    //<Booking>
    //<Guest>
    //<History>
    //<Transaction>
    //<Score>
    //<Member>
    //<Club> 
    //<Round>
}
