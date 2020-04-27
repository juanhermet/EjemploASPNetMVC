using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCPSolution.Models
{
    public class Food : Product
    {
        public Food()
        {
            Type = "Food";
        }
        public override string Type { get; set; }
        public override double Price { get; set; }
    }
}
