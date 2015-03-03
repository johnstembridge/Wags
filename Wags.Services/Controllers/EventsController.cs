using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wags.DataModel;
using Wags.Services.Model;

namespace Wags.Services.Controllers
{
    [RoutePrefix("api/events")]
    public class EventsController : ApiController
    {
        //GET: api/events?year=yr
        public IEnumerable<Event> GetEventsForYear(int year = 0)
        {
            var bl = new BusinessLayer.BusinessLayer();
            return bl.GetAllEvents(year);
        }

        // GET: api/events/5/details
        [Route("{id:int}")]
        public Event GetEventDetails(int id)
        {
            var bl = new BusinessLayer.BusinessLayer();
            var e = bl.GetEventDetails(id);
            return e;
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
                Headings = new string[] { "Position", "Player", "Points", "Strokes", "Handicap", "Status" },
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
        public IEnumerable<Booking> GetBookingsForEvent(int id)
        {
            var bl = new BusinessLayer.BusinessLayer();
            var b = bl.GetBookingsForEvent(id);
            return b;
        }

        // POST: api/events
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/events/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/events/5
        public void Delete(int id)
        {
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
