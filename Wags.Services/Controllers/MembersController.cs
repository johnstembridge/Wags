using System.Collections.Generic;
using System.Web.Http;
using Wags.DataModel;

namespace Wags.Services.Controllers
{
    public class MembersController : ApiController
    {
        // GET: api/Members
        public IEnumerable<Member> Get()
        {
            var bl = new BusinessLayer.BusinessLayer();
            return bl.GetAllMembers();
        }

        // GET: api/Members/5
        public Member Get(int id)
        {
            var bl = new BusinessLayer.BusinessLayer();
            var m = bl.GetMemberById(id);
            return m;
        }

        // POST: api/Members
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Members/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Members/5
        public void Delete(int id)
        {
        }
    }
}
