using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Wags.DataModel;
using Wags.Services.Models;

namespace Wags.Services.Controllers
{
    [RoutePrefix("api/events")]
    public class EventsController : BaseApiController
    {
        //GET: api/events?year=yr
        public IList<EventModel> GetEventsForYear(int year = 0)
        {
            return BusinessLayer.GetAllEvents(year).Select(ModelFactory.Create).ToList();
        }

        // GET: api/events/5
        [Route("{id:int}")]
        public EventModel GetEventDetails(int id)
        {
            return ModelFactory.Create(BusinessLayer.GetEventDetails(id));
        }

        // GET: api/events/5/result
        [Route("{id:int}/result")]
        public Report GetEventResult(int id)
        {
            var bl = new BusinessLayer.BusinessLayer();
            var e = bl.GetEventResult(id);
            var res = new Report()
            {
                Title = e.ToString(),
                Headings = new[] { "Position", "Player", "Points", "Strokes", "Handicap", "Status" },
                Data = (e.Rounds.Count > 0) ?
                    e.Rounds
                        .First()
                        .Scores
                        .OrderBy(s => s.Position)
                        .Select(s => FormatScore(s, e.Date)).ToList()
                    :
                    new List<object[]>()
            };
            return res;
        }

        // GET: api/events/5/bookings
        [Route("{id:int}/bookings")]
        public IEnumerable<BookingModel> GetEventBookings(int id)
        {
            return BusinessLayer.GetEventBookings(id).Select(ModelFactory.Create);
        }

        // GET: api/events/5/players
        [Route("{id:int}/players")]
        public IEnumerable<PlayerModel> GetPlayersForEvent(int id)
        {
            return BusinessLayer.GetPlayersForEvent(id).Select(ModelFactory.Create);
        }

        // POST: api/events
        [Route]
        public IHttpActionResult PostEvent([FromBody]EventModel value)
        {
            var newEvent = BusinessLayer.AddEvent(ModelFactory.Parse(value));
            if (newEvent != null)
            {
                return Created(Request.RequestUri + "/" + newEvent.Id, GetEventDetails(newEvent.Id));
            }
            else
            {
                return Conflict();
            }
        }

        // PUT: api/events/5
        [Route("{id:int}")]
        public void PutEvent(int id, [FromBody]Event value)
        {
        }

        // DELETE: api/events/5
        [Route("{id:int}")]
        public void DeleteEvent(int id)
        {
            BusinessLayer.DeleteEvent(id);
        }

        private object[] FormatScore(Score score, DateTime date)
        {
            return new object[]
            {
                score.Position,
                score.Player.FullName,
                score.Points,
                score.Shots,
                score.Player.StatusAtDate(date).Handicap,
                score.Player.StatusAtDate(date).Status.ToString()
            };
        }

    }
}
