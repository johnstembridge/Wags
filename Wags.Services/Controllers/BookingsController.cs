using System.Web.Http;
using Wags.DataModel;

namespace Wags.Services.Controllers
{
    [RoutePrefix("api/bookings")]
    public class BookingsController : ApiController
    {
        // GET api/bookings/1
        [Route("{id:int}")]
        public Booking GetBooking(int id)
        {
            var bl = new BusinessLayer.BusinessLayer();
            var b = bl.GetBooking(id);
            return b;
        }

        // GET api/bookings?eventId=e&memberId=m
        [Route("")]
        public Booking GetBookingForEventAndMember(int eventId = 0, int memberId = 0)
        {
            var bl = new BusinessLayer.BusinessLayer();
            var b = bl.GetBookingForEventAndMember(eventId, memberId);
            return b;
        }

        // POST api/bookings
        public void Post([FromBody]string value)
        {
        }

        // PUT api/bookings/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/bookings/5
        public void Delete(int id)
        {
        }
    }
}
