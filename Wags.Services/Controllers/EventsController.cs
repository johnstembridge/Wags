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
    public class EventsController : ApiController
    {
        //GET: api/events
        public IEnumerable<Event> Get()
        {
            var bl = new BusinessLayer.BusinessLayer();
            return bl.GetAllEvents();
        }

        // GET: api/events/5
        //public Event Get(int id)
        //{
        //    var bl = new BusinessLayer.BusinessLayer();
        //    var e = bl.GetEvent(id);
        //    e.Trophy.Event = null;
        //    foreach (var org in e.Organisers)
        //        org.Events = null;
        //    return e;
        //}

        // GET: api/events/5/Results
        public Report GetEventResult(int id)
        {
            var bl = new BusinessLayer.BusinessLayer();
            var e = bl.GetEventResult(id);
            if (e.Trophy != null)
                e.Trophy.Event = null;
            foreach (var org in e.Organisers)
                org.Events = null;
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
