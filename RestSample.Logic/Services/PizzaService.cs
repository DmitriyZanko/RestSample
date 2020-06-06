﻿using RestSample.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using RestSample.Data.Contexts;
using RestSample.Data.Models;

namespace RestSample.Logic.Services
{
    internal class PizzaService : IPizzaService
    {
        private readonly PizzaShopContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<PizzaDto> _validator;

        public PizzaService(PizzaShopContext context, IMapper mapper, IValidator<PizzaDto> validator)
        {
            this._context = context;
            this._mapper = mapper;
            this._validator = validator;
        }

        public IEnumerable<PizzaDto> GetAll()
        {
            return _context.Pizzas.AsNoTracking()
                .ProjectToArray<PizzaDto>(_mapper.ConfigurationProvider);
        }

        public PizzaDto GetById(int id)
        {
            return _context.Pizzas.AsNoTracking()
                .Where(p => p.Id == id)
                .ProjectToSingleOrDefault<PizzaDto>(_mapper.ConfigurationProvider);
        }

        public PizzaDto Add(PizzaDto pizza)
        {
            _validator.ValidateAndThrow(pizza, "PostValidation");

            var pizzaDb = _mapper.Map<PizzaDb>(pizza);

            _context.Pizzas.Add(pizzaDb);
            _context.SaveChanges();

            return _mapper.Map<PizzaDto>(pizzaDb);
        }

        public PizzaDto Update(int id, PizzaDto pizza)
        {
            var pizzaDb = _context.Pizzas.SingleOrDefault(p => p.Id == id);
            if (pizzaDb == null)
                return null;

            pizzaDb.Name = pizza.Name;
            pizzaDb.Price = pizza.Price;

            _context.SaveChanges();

            return new PizzaDto
            {
                Id = pizzaDb.Id,
                Name = pizzaDb.Name,
                Price = pizzaDb.Price
            };
        }

        public bool Delete(int id)
        {
            var pizzaDb = _context.Pizzas.SingleOrDefault(p => p.Id == id);
            if (pizzaDb == null)
                return false;

            _context.Pizzas.Remove(pizzaDb);
            _context.SaveChanges();

            return true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                _context.Dispose();
                GC.SuppressFinalize(this);
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PizzaService()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
