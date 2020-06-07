using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using RestSample.Logic.Models;

namespace RestSample.Logic.Services
{
    public interface IPizzaService: IDisposable
    {
        Result<IEnumerable<PizzaDto>> GetAll();

        Result<Maybe<PizzaDto>> GetById(int id);
        
        Result<PizzaDto> Add(PizzaDto pizza);

        Result<Maybe<PizzaDto>> Update(int id, PizzaDto pizza);

        Result<bool> Delete(int id);
    }
}