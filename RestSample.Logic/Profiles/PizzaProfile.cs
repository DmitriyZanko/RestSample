using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestSample.Data.Models;
using RestSample.Logic.Models;

namespace RestSample.Logic.Profiles
{
    class PizzaProfile : Profile
    {
        public PizzaProfile()
        {
            CreateMap<PizzaDb, PizzaDto>()
                .ReverseMap();
        }
        // Mapster
    }

    class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<IngredientDb, IngredientDto>()
                .ReverseMap();
        }
        
    }
}
