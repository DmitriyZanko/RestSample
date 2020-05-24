using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSample.Data.Models
{
    public class PizzaDb
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int Price { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? UpdaterId { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
