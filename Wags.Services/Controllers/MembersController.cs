using System.Collections.Generic;
using System.Web.Http;
using Wags.DataModel;

namespace Wags.Services.Controllers
{
    [RoutePrefix("api/members")]
    public class MembersController : ApiController
    {
        // GET: api/members[?current=true|false]
        public IEnumerable<Member> GetAllMembers(bool current=true)
        {
            var bl = new BusinessLayer.BusinessLayer();
            return bl.GetAllMembers(current);
        }

        //// GET: api/members?name=n
        //public Member GetMemberByName(string name = "")
        //{
        //    var bl = new BusinessLayer.BusinessLayer();
        //    return bl.GetMemberByName(name);
        //}

        // GET: api/members/5
        [Route("{id:int}")]
        public Member GetMember(int id)
        {
            var bl = new BusinessLayer.BusinessLayer();
            var m = bl.GetMemberById(id);
            return m;
        }

        // GET: api/members/5/history
        [Route("{id:int}/history")]
        public Member GetMemberHistory(int id)
        {
            var bl = new BusinessLayer.BusinessLayer();
            var m = bl.GetMemberById(id);
            return m;
        }

        // POST: api/members
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/members/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/members/5
        public void Delete(int id)
        {
        }
    }
}
