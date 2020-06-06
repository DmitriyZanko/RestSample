using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSample.Data.Contexts.Configuration;
using RestSample.Data.Migrations;
using RestSample.Data.Models;

namespace RestSample.Data.Contexts
{
    public sealed class PizzaShopContext : DbContext
    {
        public PizzaShopContext()
        {
            Database.SetInitializer<PizzaShopContext>(new MigrateDatabaseToLatestVersion<PizzaShopContext, Migrations.Configuration>());
            Database.Log = msg => Debug.WriteLine(msg);
        }

        public  DbSet<PizzaDb> Pizzas { get; set; }

        public DbSet<IngredientDb> Ingredients{ get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(typeof(PizzaShopContext).Assembly); // PizzaShopContext - any type from assembly
        }
    }
}
