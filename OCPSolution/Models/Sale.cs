using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCPSolution.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
    }
}
