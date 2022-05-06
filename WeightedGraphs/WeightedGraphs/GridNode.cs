using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightedGraphs
{
    internal class GridNode : IHasPosition
    {
        public double X { get; set; }
        public double Y { get; set; }
        public override string ToString()
        {
            return $"X:{X}, Y:{Y}";
        }
        public GridNode(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
