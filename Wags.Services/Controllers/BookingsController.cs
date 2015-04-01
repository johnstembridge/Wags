using System;
using System.Web.Http;
using Wags.DataModel;
using Wags.Services.Models;

namespace Wags.Services.Controllers
{
    [RoutePrefix("api/bookings")]
    public class BookingsController : BaseApiController
    {
        // GET api/bookings/1
        [Route("{id:int}")]
        public IHttpActionResult GetBooking(int id)
        {
            try
            {
                var booking = BusinessLayer.GetBooking(id);
                if (booking != null)
                {
                    return Ok(MakeBookingModel(booking));
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

        // GET api/bookings?eventId=e&playerId=m
        [Route("")]
        public IHttpActionResult GetBookingForEventAndMember(int eventId = 0, int memberId = 0)
        {
            try
            {
                var booking = BusinessLayer.GetBookingForEventAndMember(eventId, memberId);
                if (booking != null)
                {
                    return Ok(MakeBookingModel(booking));
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

        // POST api/bookings
        [Route]
        public IHttpActionResult Post([FromBody]BookingModel value)
        {
            try
            {
                var newBooking = ModelFactory.Parse(value);
                if (newBooking == null)
                    return BadRequest("Could not read course details from body");
                var res = BusinessLayer.AddBooking(newBooking);
                if (res != null)
                {
                    var response = ModelFactory.Create(BusinessLayer.GetBooking(res.Id));
                    return CreatedAtRoute("DefaultApi", new { controller = "bookings", id = res.Id }, response);
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

        // PUT api/bookings/5
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]BookingModel value)
        {
            try
            {
                var updatedBooking = ModelFactory.Parse(value);
                if (updatedBooking == null)
                    return BadRequest("Could not read booking details from body");

                if (BusinessLayer.BookingExists(id))
                {
                    BusinessLayer.UpdateBooking(updatedBooking);
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

        // DELETE api/bookings/5
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (BusinessLayer.BookingExists(id))
                {
                    BusinessLayer.DeleteBooking(id);
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

        BookingModel MakeBookingModel(Booking booking)
        {
            var res = ModelFactory.Create(booking);
            res.AddLink("self", FullPath(""));
            res.AddLink("event", FullPath("../../events/" + res.EventId));
            return res;

        }
    }
}
