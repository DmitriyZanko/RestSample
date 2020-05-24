using RestSample.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSample.Logic.Services
{
    internal class PizzaService : IPizzaService
    {
        private static readonly List<PizzaDto> _pizzas = PizzaFaker.Generate(10);

        public IEnumerable<PizzaDto> GetAll()
        {
            return _pizzas;
        }

        public PizzaDto GetById(int id)
        {
            return _pizzas.SingleOrDefault(p => p.Id == id);
        }

        public PizzaDto Add(PizzaDto pizza)
        {
            var id = _pizzas.Last().Id + 1;
            pizza.Id = id;
            _pizzas.Add(pizza);
            return pizza;
        }

        public PizzaDto Update(int id, PizzaDto pizza)
        {
            var pizzaTemp = _pizzas.SingleOrDefault(p => p.Id == id);
            if (pizzaTemp == null)
                return null;

            pizzaTemp.Name = pizza.Name;
            pizzaTemp.Price = pizza.Price;
            return pizzaTemp;
        }

        public bool Delete(int id)
        {
            var pizzaTemp = _pizzas.SingleOrDefault(p => p.Id == id);
            if (pizzaTemp == null)
                return false;

            return _pizzas.Remove(pizzaTemp);
        }
    }
}
