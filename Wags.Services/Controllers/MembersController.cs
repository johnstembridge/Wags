using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Wags.Services.Models;

namespace Wags.Services.Controllers
{
    [RoutePrefix("api/members")]
    public class MembersController : BaseApiController
    {
        // GET: api/members[?current=true|false]
        public IEnumerable<MemberModel> GetAllMembers(bool current=true)
        {
            var bl = new BusinessLayer.BusinessLayer();
            return bl.GetAllMembers(current).Select(ModelFactory.Create);
        }

        //// GET: api/members?name=n
        //public Member GetMemberByName(string name = "")
        //{
        //    var bl = new BusinessLayer.BusinessLayer();
        //    return bl.GetMemberByName(name);
        //}

        // GET: api/members/5
        [Route("{id:int}")]
        public IHttpActionResult GetMember(int id)
        {
            try
            {
                var member = BusinessLayer.GetMember(id);
                if (member != null)
                {
                    var res = ModelFactory.Create(member);
                    res.AddLink("self", FullPath(""));
                    res.AddLink("history", FullPath("history"));
                    res.AddLink("transactions", FullPath("transactions"));
                    res.AddLink("bookings", FullPath("bookings"));
                    res.AddLink("events", FullPath("events"));
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

        // GET: api/members/5/history
        [Route("{id:int}/history")]
        public IHttpActionResult GetMemberHistory(int id)
        {
            try
            {
                var history = BusinessLayer.GetMemberHistory(id);
                if (history != null)
                {
                    var res = history.Select(ModelFactory.Create);
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

        // POST: api/members
        public IHttpActionResult Post([FromBody]MemberModel value)
        {
            try
            {
                var newMember = ModelFactory.Parse(value);
                if (newMember == null)
                    return BadRequest("Could not read member details from body");
                var res = BusinessLayer.AddMember(newMember);
                if (res != null)
                {
                    var response = ModelFactory.Create(BusinessLayer.GetMember(res.Id));
                    return CreatedAtRoute("DefaultApi", new { controller = "members", id = res.Id }, response);
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

        // PUT: api/members/5
        public IHttpActionResult Put(int id, [FromBody]MemberModel value)
        {
            try
            {
                var newMember = ModelFactory.Parse(value);
                if (newMember == null)
                    return BadRequest("Could not read member details from body");

                if (BusinessLayer.MemberExists(id))
                {
                    BusinessLayer.UpdateMember(newMember);
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

        // DELETE: api/members/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (BusinessLayer.MemberExists(id))
                {
                    BusinessLayer.DeleteMember(id);
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
    }
}
