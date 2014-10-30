using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wags.DataModel;

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

        //// GET: api/events/5
        //public Member Get(int id)
        //{
        //    var bl = new BusinessLayer.BusinessLayer();
        //    var m = bl.GetMemberById(id);
        //    m.Player.Member = null;
        //    return m;
        //}

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
    }
}
