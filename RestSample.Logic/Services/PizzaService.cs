using RestSample.Logic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using FluentValidation;
using JetBrains.Annotations;
using RestSample.Data.Contexts;
using RestSample.Data.Models;

namespace RestSample.Logic.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly PizzaShopContext _context;
        private readonly IMapper _mapper;

        public PizzaService([NotNull]PizzaShopContext context, [NotNull]IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public Result<IEnumerable<PizzaDto>> GetAll()
        {
            try
            {
                var pizzas = _context.Pizzas.AsNoTracking()/*.Include(p => p.Ingredients)*/.ToArray();
                return Result.Success(_mapper.Map<IEnumerable<PizzaDto>>(pizzas));
            }
            catch (SqlException ex)
            {
                return Result.Failure<IEnumerable<PizzaDto>>(ex.Message);
            }
        }

        public Result<Maybe<PizzaDto>> GetById(int id)
        {
            try
            {
                Maybe<PizzaDto> pizza = _context.Pizzas.AsNoTracking()
                    .Where(p => p.Id == id)
                    .ProjectToSingleOrDefault<PizzaDto>(_mapper.ConfigurationProvider);

                return Result.Success(pizza);

            }
            catch (SqlException ex)
            {
                return Result.Failure<Maybe<PizzaDto>>(ex.Message);
            }
        }

        public Result<PizzaDto> Add(PizzaDto pizza)
        {
            try
            {
                var pizzaDb = _mapper.Map<PizzaDb>(pizza);

                _context.Pizzas.Add(pizzaDb);
                _context.SaveChanges();

                return Result.Success(_mapper.Map<PizzaDto>(pizzaDb));
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<PizzaDto>(ex.Message);
            }
        }

        public Result<Maybe<PizzaDto>> Update(int id, PizzaDto pizza)
        {
            try
            {
                var pizzaDb = _context.Pizzas.SingleOrDefault(p => p.Id == id);
                if (pizzaDb == null)
                    return Result.Success(_mapper.Map<Maybe<PizzaDto>>(null));

                pizzaDb.Name = pizza.Name;
                pizzaDb.Price = pizza.Price;

                _context.SaveChanges();

                return Result.Success(_mapper.Map<Maybe<PizzaDto>>(pizzaDb));
            }
            catch (SqlException ex)
            {
                return Result.Failure<Maybe<PizzaDto>>(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<Maybe<PizzaDto>>(ex.Message);
            }
        }

        public Result<bool> Delete(int id)
        {
            try
            {
                var pizzaDb = _context.Pizzas.SingleOrDefault(p => p.Id == id);
                if (pizzaDb == null)
                    return Result.Success(false);

                _context.Pizzas.Remove(pizzaDb);
                _context.SaveChanges();

                return Result.Success(true);
            }
            catch (SqlException ex)
            {
                return Result.Failure<bool>(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<bool>(ex.Message);
            }
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
