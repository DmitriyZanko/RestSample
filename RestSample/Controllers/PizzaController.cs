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
            return Ok(_pizzaService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var pizza = _pizzaService.GetById(id);
            return pizza == null ? (IHttpActionResult)NotFound() : Ok(pizza);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody, CustomizeValidator(RuleSet = "PreValidator")]PizzaDto pizza)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var pizzaResult = _pizzaService.Add(pizza);
                return Created($"api/pizzas/{pizzaResult.Id}", pizzaResult);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

       [HttpPut]
       [Route("{id}")]
        public IHttpActionResult Update(int id, [FromBody] PizzaDto pizza)
        {
            var pizzaTemp = _pizzaService.Update(id, pizza);
            if (pizzaTemp == null)
                return NotFound();

            return Ok(pizzaTemp);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = _pizzaService.Delete(id);
            if (!result)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
