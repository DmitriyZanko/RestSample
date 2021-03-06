﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSample.Data.Models
{

    public class Entity
    {
        public int Id { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? UpdaterId { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }

    public class PizzaDb : Entity
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public int Weight { get; set; }

        public ICollection<IngredientDb> Ingredients { get; set; }
    }

    public class IngredientDb
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
