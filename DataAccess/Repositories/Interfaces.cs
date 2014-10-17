using Wags.DataModel;

namespace Wags.DataAccess
{
    public interface IPlayerRepository : IGenericDataRepository<Player>
    {
    }

    public interface ICourseRepository : IGenericDataRepository<Course>
    {
    }

    public interface IMemberRepository : IGenericDataRepository<Member>
    {
    }

    public interface ICourseDataRepository : IGenericDataRepository<CourseData>
    {
    }

    public interface IEventRepository : IGenericDataRepository<Event>
    {
    }

    public interface ITrophyRepository : IGenericDataRepository<Trophy>
    {
    }

    public interface IBookingRepository : IGenericDataRepository<Booking>
    {
    }

    public interface IGuestRepository : IGenericDataRepository<Guest>
    {
    }

    public interface IHistoryRepository : IGenericDataRepository<History>
    {
    }

    public interface ITransactionRepository : IGenericDataRepository<Transaction>
    {
    }

    public interface IScoreRepository : IGenericDataRepository<Score>
    {
    }

    public interface IClubRepository : IGenericDataRepository<Club>
    {
    }

    public interface IRoundRepository : IGenericDataRepository<Round>
    {
    }

}