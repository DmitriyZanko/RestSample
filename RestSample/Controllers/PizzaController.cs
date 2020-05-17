using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace RestSample.Controllers
{
    public class PizzaDto // DTO Data Transfer Object
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }

    [RoutePrefix("api/pizzas")]
    public class PizzaController : ApiController
    {
        private static List<PizzaDto> _pizzas = new List<PizzaDto>
        {
            new PizzaDto {Id = 1, Name = "Margarita", Price = 10},
            new PizzaDto {Id = 2, Name = "Peperoni", Price = 22}
        };

        // Select
        // Get All
        // Get by id
        // Get by filter

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_pizzas);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var pizza = _pizzas.FirstOrDefault(p => p.Id == id);
            return pizza == null ? (IHttpActionResult)NotFound() : Ok(pizza);
        }

        // Insert
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]PizzaDto pizza)
        {
            var id = _pizzas.Last().Id + 1;
            pizza.Id = id;
            _pizzas.Add(pizza);
            return Created($"api/pizzas/{id}", pizza);
        }

        // Update
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update(int id, [FromBody] PizzaDto pizza)
        {
            var pizzaTemp = _pizzas.FirstOrDefault(p => p.Id == id);
            if (pizzaTemp == null) 
                return NotFound();

            pizzaTemp.Name = pizza.Name;
            pizzaTemp.Price = pizza.Price;
            return Ok(pizza);
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            var pizza = _pizzas.FirstOrDefault(p => p.Id == id);
            if (pizza == null)
                return NotFound();

            _pizzas.Remove(pizza);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
