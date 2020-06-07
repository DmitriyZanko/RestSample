using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using FluentValidation;
using FluentValidation.WebApi;
using RestSample.Logic.Models;
using RestSample.Logic.Services;

namespace RestSample.Controllers
{
    [RoutePrefix("api/pizzas")]
    public class PizzaController : ApiController
    {
        private IPizzaService _pizzaService;

        public PizzaController(IPizzaService pizzaService)
        {
            this._pizzaService = pizzaService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var result = _pizzaService.GetAll();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var result = _pizzaService.GetById(id);
            
            if (result.IsFailure)
                return StatusCode(HttpStatusCode.InternalServerError);

            return result.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(result.Value.Value);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody, CustomizeValidator(RuleSet = "PreValidator")]PizzaDto pizza)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _pizzaService.Add(pizza);
            return result.IsSuccess ? 
                Created($"api/pizzas/{result.Value.Id}", result.Value) : 
                (IHttpActionResult)BadRequest(result.Error);
        }

       [HttpPut]
       [Route("{id}")]
        public IHttpActionResult Update(int id, [FromBody] PizzaDto pizza)
        {
            var result = _pizzaService.Update(id, pizza);
           
            if (result.IsFailure)
                return StatusCode(HttpStatusCode.InternalServerError);

            return result.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(result.Value.Value);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _pizzaService.Delete(id);

            if (result.IsFailure)
                return StatusCode(HttpStatusCode.InternalServerError);

            if (result.Value)
                return StatusCode(HttpStatusCode.NoContent);

            return NotFound();
        }
    }
}
