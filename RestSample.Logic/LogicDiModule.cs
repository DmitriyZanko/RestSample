using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Ninject.Modules;
using RestSample.Data.Contexts;
using RestSample.Logic.Models;
using RestSample.Logic.Profiles;
using RestSample.Logic.Services;
using RestSample.Logic.Validators;

namespace RestSample.Logic
{
    public class LogicDiModule: NinjectModule
    {
        public override void Load()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(PizzaProfile)));
            var mapper = Mapper.Configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper);

            this.Bind<PizzaShopContext>().ToSelf();
            this.Bind<IPizzaService>().To<PizzaService>();
            this.Bind<IValidator<PizzaDto>>().To<PizzaDtoValidator>();
            // ...
        }
    }
}
