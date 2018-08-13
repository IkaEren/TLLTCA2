using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using CA1AssignmentTask3.Models;
using CA1AssignmentTask3.Filters;

namespace CA1AssignmentTask3.Controllers
{
    public class TalentsController : ApiController
    {
        static readonly TalentRepository repository = new TalentRepository();

        [EnableCors(origins: "*", headers: "*", methods: "*")]

        [RequireHttps] 
        // HTTPS is used to secure the web API for Task 4
        [HttpGet]
        [Route("api/talents")]
        // https://localhost:44308/api/talents GET Request, have to use HTTPS if not it'll not be found
        public IEnumerable<Talent> GetAllTalents()
        {
            return repository.GetAll();
        }

        [HttpGet]
        [Route("api/talents/{id:int}", Name = "getTalentById")]
        // http://localhost:51123/api/talents/4 GET Request
        public Talent GetTalent(int id)
        {
            Talent item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        [HttpPost]
        [Route("api/talents")]
        // http://localhost:51123/api/talents/ POST Request
        public HttpResponseMessage PostTalent(Talent item)
        {
            if (ModelState.IsValid)
            {
                item = repository.Add(item);
                var response = Request.CreateResponse<Talent>(HttpStatusCode.Created, item);

                string uri = Url.Link("getTalentById", new { id = item.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("api/talents/{id:int}")]
        // http://localhost:51123/api/talents/4 PUT Request
        public HttpResponseMessage PutTalent(int id, Talent talent)
        {
            talent.Id = id;
            if (!repository.Update(talent))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpDelete]
        [Route("api/talents/{id:int}")]
        // http://localhost:51123/api/talents/4 DELETE Request
        public HttpResponseMessage DeleteTalent(int id)
        {
            if (ModelState.IsValid)
            {
                repository.Remove(id);
                var response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ModelState);
            }
        }
    }
}
