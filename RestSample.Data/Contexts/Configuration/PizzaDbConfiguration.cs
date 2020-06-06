using System.Data.Entity.ModelConfiguration;
using RestSample.Data.Models;

namespace RestSample.Data.Contexts.Configuration
{
    internal class PizzaDbConfiguration : EntityTypeConfiguration<PizzaDb>
    {
        public PizzaDbConfiguration()
        {
            HasKey(x => x.Id).ToTable("ShopPizzas");
            Property(x => x.Name).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();
            Property(x => x.Price).IsRequired();
            HasMany(x => x.Ingredients).WithOptional();
        }
    }
}