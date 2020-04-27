using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCPSolution.Models
{
    public class Row
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public Sale Sale { get; set; }
        public Product Product { get; set; }
    }
}
