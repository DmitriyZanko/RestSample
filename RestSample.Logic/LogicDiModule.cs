﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using RestSample.Data.Contexts;
using RestSample.Logic.Services;

namespace RestSample.Logic
{
    public class LogicDiModule: NinjectModule
    {
        public override void Load()
        {
            this.Bind<PizzaShopContext>().ToSelf();
            this.Bind<IPizzaService>().To<PizzaService>();
            // ...
        }
    }
}
