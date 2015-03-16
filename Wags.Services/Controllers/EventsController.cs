using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
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
        public HttpResponseMessage GetEventDetails(int id)
        {
            try
            {
                var eventObj = BusinessLayer.GetEventDetails(id);
                if (eventObj != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ModelFactory.Create(eventObj));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET: api/events/5/bookings
        [Route("{id:int}/bookings")]
        public HttpResponseMessage GetEventBookings(int id)
        {
            try
            {
                var bookings = BusinessLayer.GetEventBookings(id);
                if (bookings != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, bookings.Select(ModelFactory.Create));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET: api/events/5/players
        [Route("{id:int}/players")]
        public HttpResponseMessage GetPlayersForEvent(int id)
        {
            try
            {
                var players = BusinessLayer.GetPlayersForEvent(id);
                if (players != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, players.Select(ModelFactory.Create));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }            

        // GET: api/events/5/result
        [Route("{id:int}/result")]
        public HttpResponseMessage GetEventResult(int id)
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
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST: api/events
        [Route]
        public HttpResponseMessage PostEvent([FromBody]EventModel value)
        {
            try
            {
                var newEvent = ModelFactory.Parse(value);
                if (newEvent == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "Could not read event details from body");
                var res = BusinessLayer.AddEvent(newEvent);
                if (res != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, ModelFactory.Create(BusinessLayer.GetEventDetails(res.Id)));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Duplicate event");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT: api/events/5
        [Route("{id:int}")]
        public HttpResponseMessage PutEvent(int id, [FromBody]EventModel value)
        {
            try
            {
                var updatedEvent = ModelFactory.Parse(value);
                if (updatedEvent == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read event details from body");

                if (BusinessLayer.EventExists(id))
                {
                    BusinessLayer.UpdateEvent(updatedEvent);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Event not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE: api/events/5
        [Route("{id:int}")]
        public HttpResponseMessage DeleteEvent(int id)
        {
            try
            {
                if (BusinessLayer.EventExists(id))
                {
                    BusinessLayer.DeleteEvent(id);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
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
