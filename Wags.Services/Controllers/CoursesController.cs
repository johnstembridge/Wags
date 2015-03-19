using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Wags.Services.Models;

namespace Wags.Services.Controllers
{
    [RoutePrefix("api/Courses")]
    public class CoursesController : BaseApiController
    {
        // GET: api/Courses?club=clubName
        [Route]
        public IEnumerable<CourseModel> GetAllCourses(string clubName = "")
        {
            return BusinessLayer.GetAllCourses(clubName).Select(ModelFactory.Create);
        }

        // GET: api/Courses/5
        [Route("{id:int}")]
        public IHttpActionResult GetCourseAll(int id)
        {
            try
            {
                var course = BusinessLayer.GetCourseAll(id);
                if (course != null)
                {
                    var res = ModelFactory.Create(course);
                    res.AddLink("self", FullPath(""));
                    res.AddLink("rounds", FullPath("rounds"));
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

        // GET: api/Courses/5/rounds
        [Route("{id:int}/rounds")]
        public IHttpActionResult GetCourseRounds(int id)
        {
            try
            {
                var rounds = BusinessLayer.GetCourseRounds(id);
                if (rounds != null)
                {
                    return Ok(rounds.Select(ModelFactory.Create));
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

        // POST: api/Courses
        public IHttpActionResult Post([FromBody]CourseModel value)
        {
            try
            {
                var newCourse = ModelFactory.Parse(value);
                if (newCourse == null)
                    return BadRequest("Could not read course details from body");
                var res = BusinessLayer.AddCourse(newCourse);
                if (res != null)
                {
                    var response = ModelFactory.Create(BusinessLayer.GetCourseAll(res.Id));
                    return CreatedAtRoute("DefaultApi", new { controller = "courses", id = res.Id }, response);
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

        // PUT: api/Courses/5
        public IHttpActionResult Put(int id, [FromBody]CourseModel value)
        {
            try
            {
                var updatedCourse = ModelFactory.Parse(value);
                if (updatedCourse == null)
                    return BadRequest("Could not read course details from body");

                if (BusinessLayer.EventExists(id))
                {
                    BusinessLayer.UpdateCourse(updatedCourse);
                    return Ok();
                }
                else
                {
                    return StatusCode(HttpStatusCode.NotModified);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // DELETE: api/Courses/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (BusinessLayer.EventExists(id))
                {
                    BusinessLayer.DeleteCourse(id);
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
