using System;
using System.Web.Http;
using Wags.DataModel;
using Wags.Services.Models;

namespace Wags.Services.Controllers
{
    [RoutePrefix("api/scores")]
    public class ScoresController : BaseApiController
    {
        // GET api/scores/1
        [Route("{id:int}")]
        public IHttpActionResult GetScore(int id)
        {
            try
            {
                var score = BusinessLayer.GetScore(id);
                if (score != null)
                {
                    return Ok(MakeScoreModel(score));
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

        // GET api/scores?roundId=e&playerId=m
        [Route("")]
        public IHttpActionResult GetScoreForRoundAndPlayer(int roundId = 0, int playerId = 0)
        {
            try
            {
                var score = BusinessLayer.GetScoreForRoundAndPlayer(roundId, playerId);
                if (score != null)
                {
                    return Ok(MakeScoreModel(score));
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

        // POST api/scores
        [Route]
        public IHttpActionResult Post([FromBody]ScoreModel value)
        {
            try
            {
                var newscore = ModelFactory.Parse(value);
                if (newscore == null)
                    return BadRequest("Could not read score details from body");
                var res = BusinessLayer.AddScore(newscore);
                if (res != null)
                {
                    var response = ModelFactory.Create(BusinessLayer.GetScore(res.Id));
                    return CreatedAtRoute("DefaultApi", new { controller = "scores", id = res.Id }, response);
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

        // PUT api/scores/5
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]ScoreModel value)
        {
            try
            {
                var updatedscore = ModelFactory.Parse(value);
                if (updatedscore == null)
                    return BadRequest("Could not read score details from body");

                if (BusinessLayer.ScoreExists(id))
                {
                    BusinessLayer.UpdateScore(updatedscore);
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

        // DELETE api/scores/5
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (BusinessLayer.ScoreExists(id))
                {
                    BusinessLayer.DeleteScore(id);
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

        ScoreModel MakeScoreModel(Score score)
        {
            var res = ModelFactory.Create(score);
            res.AddLink("self", FullPath(""));
            res.AddLink("player", FullPath("../../players/" + score.PlayerId));
            res.AddLink("event", FullPath("../../events/" + score.Round.EventId));
            res.AddLink("round", FullPath("../../rounds/" + res.RoundId));
            res.AddLink("result", FullPath("../../events/" + score.Round.EventId + "/results"));
            return res;

        }
    }
}
