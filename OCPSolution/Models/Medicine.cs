using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCPSolution.Models
{
    public class Medicine : Product
    {
        public Medicine()
        {
            Type = "Medicine";

        }
        private double desc = 0.8;
        public override string Type { get; set; }
        public override double Price { get; set; }
        public double Desc { get => Price * desc; }
    }
}
