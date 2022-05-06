using System;

namespace WeightedGraphs
{
    internal class Program
    {
        static void CreateGraph(Graph<GridNode> graph)
        {
            Dictionary<(int x, int y), Vertex<GridNode>> grid = new Dictionary<(int x, int y), Vertex<GridNode>>();
            const int gridWidth = 10;
            const int gridHeight = 10; 
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    var v = new Vertex<GridNode>(new GridNode(x, y));
                    graph.AddVertex(v);
                    grid.Add((x, y), v);
                }
            }
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    if(x > 0)
                    {
                        graph.AddEdge(grid[(x, y)], grid[(x - 1, y)], 1);
                    }
                    if(x < gridWidth - 1)
                    {
                        graph.AddEdge(grid[(x, y)], grid[(x + 1, y)], 1);
                    }
                    if(y > 0)
                    {
                        graph.AddEdge(grid[(x, y)], grid[(x, y - 1)], 1);
                    }
                    if(y < gridHeight - 1)
                    {
                        graph.AddEdge(grid[(x, y)], grid[(x, y + 1)], 1);
                    }
                }
            }
            //make loop for connetions
        }
        static void Main(string[] args)
        {
            Graph<GridNode> graph = new Graph<GridNode>();
            List<GridNode> list = new List<GridNode>();
            CreateGraph(graph);
            list = graph.AStarSearchAlgorithm(graph.Vertices[0], graph.Vertices[9]);
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].ToString());
            }
        }
    }
}
