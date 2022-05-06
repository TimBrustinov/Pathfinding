using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightedGraphs
{
    public interface IHasPosition
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
