using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCPSolution.Models
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public abstract string Type { get; set; }
        public DateTime DueDate { get; set; }
        public string Provider { get; set; }
        public abstract double Price { get; set; }

    }
}
