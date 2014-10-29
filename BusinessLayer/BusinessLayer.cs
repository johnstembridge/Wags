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
            return GetMember(d => d.FirstName == names[0] && d.LastName == names[1]);
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
