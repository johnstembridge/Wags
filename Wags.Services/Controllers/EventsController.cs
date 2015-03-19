using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Wags.DataModel;
using Wags.Services.Models;

namespace Wags.Services.Controllers
{
    [RoutePrefix("api/events")]
    public class EventsController : BaseApiController
    {
        //GET: api/events?year=yr
        [Route]
        public IEnumerable<EventModel> GetEventsForYear(int year = 0)
        {
            return BusinessLayer.GetAllEvents(year).Select(ModelFactory.Create);
        }

        // GET: api/events/5
        [Route("{id:int}")]
        public IHttpActionResult GetEventDetails(int id)
        {
            try
            {
                var eventObj = BusinessLayer.GetEventDetails(id);
                if (eventObj != null)
                {
                    var res = ModelFactory.Create(eventObj);
                    res.AddLink("self", FullPath(""));
                    res.AddLink("players", FullPath("players"));
                    res.AddLink("bookings", FullPath("bookings"));
                    res.AddLink("results", FullPath("results"));
                    return Ok(res);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // GET: api/events/5/bookings
        [Route("{id:int}/bookings")]
        public IHttpActionResult GetEventBookings(int id)
        {
            try
            {
                var bookings = BusinessLayer.GetEventBookings(id);
                if (bookings != null)
                {
                    return Ok(bookings.Select(ModelFactory.Create));
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // GET: api/events/5/players
        [Route("{id:int}/players")]
        public IHttpActionResult GetPlayersForEvent(int id)
        {
            try
            {
                var players = BusinessLayer.GetPlayersForEvent(id);
                if (players != null)
                {
                    return Ok(players.Select(ModelFactory.Create));
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }            

        // GET: api/events/5/results
        [Route("{id:int}/results")]
        public IHttpActionResult GetEventResult(int id)
        {
            try
            {
                var e = BusinessLayer.GetEventResult(id);
                if (e != null)
                {
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
                    return Ok(res);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // POST: api/events
        [Route]
        public IHttpActionResult PostEvent([FromBody]EventModel value)
        {
            try
            {
                var newEvent = ModelFactory.Parse(value);
                if (newEvent == null)
                    return BadRequest("Could not read event details from body");
                var res = BusinessLayer.AddEvent(newEvent);
                if (res != null)
                {
                    var response = ModelFactory.Create(BusinessLayer.GetEventDetails(res.Id));
                    return CreatedAtRoute("DefaultApi", new {controller="events", id=res.Id}, response);
                }
                else
                {
                    return Conflict();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // PUT: api/events/5
        [Route("{id:int}")]
        public IHttpActionResult PutEvent(int id, [FromBody]EventModel value)
        {
            try
            {
                var updatedEvent = ModelFactory.Parse(value);
                if (updatedEvent == null)
                    return BadRequest("Could not read event details from body");

                if (BusinessLayer.EventExists(id))
                {
                    BusinessLayer.UpdateEvent(updatedEvent);
                    return Ok();
                }
                else
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // DELETE: api/events/5
        [Route("{id:int}")]
        public IHttpActionResult DeleteEvent(int id)
        {
            try
            {
                if (BusinessLayer.EventExists(id))
                {
                    BusinessLayer.DeleteEvent(id);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
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
