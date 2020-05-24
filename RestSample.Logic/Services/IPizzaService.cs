using System.Collections.Generic;
using RestSample.Logic.Models;

namespace RestSample.Logic.Services
{
    public interface IPizzaService
    {
        IEnumerable<PizzaDto> GetAll();
        PizzaDto GetById(int id);
        PizzaDto Add(PizzaDto pizza);
        PizzaDto Update(int id, PizzaDto pizza);
        bool Delete(int id);
    }
}