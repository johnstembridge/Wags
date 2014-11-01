using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wags.DataModel;

namespace Wags.Services.Controllers
{
    public class BookingsController : ApiController
    {
        // GET api/bookings/1
        public Booking Get(int id)
        {
            var bl = new BusinessLayer.BusinessLayer();
            var b = bl.GetBooking(id);
            b.Event.Bookings = null;
            b.Member.Bookings = null;
            return b;
        }

        // GET api/bookings/5/1
        //public Booking Get(int eventId, int memberId)
        //{
        //    var bl = new BusinessLayer.BusinessLayer();
        //    var b = bl.GetBooking(eventId, memberId);
        //    return b;
        //}

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
