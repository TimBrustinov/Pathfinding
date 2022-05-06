using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightedGraphs
{
    internal class Graph<T> where T : IHasPosition
    {
        private List<Vertex<T>> vertices;

        public IReadOnlyList<Vertex<T>> Vertices => vertices;
        public int VertexCount => vertices.Count;

        public Graph()
        {
            vertices = new List<Vertex<T>>();
        }
        public void AddVertex(Vertex<T> vertex)
        {
            if (vertex == null || vertex.Edges.Count != 0 || vertices.Contains(vertex))
            {
                return;
            }
            vertices.Add(vertex);
        }
        public bool AddEdge(Vertex<T> A, Vertex<T> B, double distance)
        {
            if (A == null || B == null)
            {
                return false;
            }
            else if (!vertices.Contains(A) || !vertices.Contains(B))
            {
                return false;
            }
            else if (A.Edges.ContainsKey(B) || B.Edges.ContainsKey(A))
            {
                return false;
            }
            else
            {
                A.Edges.Add(B, distance);
                return true;
            }
        }
        public bool RemoveVertex(Vertex<T> vertex)
        {
            if (vertices.Contains(vertex))
            {
                vertices.Remove(vertex);
                for (int i = 0; i < vertices.Count; i++)
                {
                    if (vertices[i].Edges.ContainsKey(vertex))
                    {
                        vertices[i].Edges.Remove(vertex);
                    }
                }
                return true;
            }
            return false;
        }
        public bool RemoveEdge(Vertex<T> A, Vertex<T> B)
        {
            if (!vertices.Contains(A) || !vertices.Contains(B) || A.Edges.ContainsKey(B))
            {
                return false;
            }
            A.Edges.Remove(B);
            return true;
        }
        public bool search(Vertex<T> vertex)
        {
            if(vertices.Contains(vertex))
            {
                return true;
            }
            return false;
        }
        public List<List<Vertex<T>>> GetAllPaths(Vertex<T> start, Vertex<T> end)
        {
            start.wasVisited = true;
            List<Vertex<T>> currentPath = new List<Vertex<T>>();
            currentPath.Add(start);
            List<List<Vertex<T>>> allPaths = new List<List<Vertex<T>>>();
            GetAllPaths(start, end, currentPath, allPaths);

            return allPaths;
        }
        private void GetAllPaths(Vertex<T> curr, Vertex<T> end, List<Vertex<T>> currentPath, List<List<Vertex<T>>> allPaths)
        {
            if (curr == end)
            {
                allPaths.Add(new List<Vertex<T>>(currentPath));
                return;
            }

            //Loop through current's neighbors and DepthFirstSearch with an updated current and the same end
            foreach (Vertex<T> kvp in curr.Edges.Keys)
            {
                //Add to the list
                if (kvp.wasVisited)
                {
                    continue;
                }

                currentPath.Add(kvp);
                kvp.wasVisited = true;
                GetAllPaths(kvp, end, currentPath, allPaths);
                currentPath.Remove(kvp);
                kvp.wasVisited = false;
                //curr.Neighbors[i].wasVisited = false;
                //Remove that node we just added since it is obviously not part of the path
            }
        }
        public List<T> DijkstraAlgorithm(Vertex<T> start, Vertex<T> end)
        {
            PriorityQueue<Vertex<T>, double> priorityQueue = new PriorityQueue<Vertex<T>, double>();
            Vertex<T> current = start;
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i].cumalativeDistance = double.MaxValue;
                vertices[i].wasVisited = false;
            }
            start.cumalativeDistance = 0;
            priorityQueue.Enqueue(start, 0);
            while (current != end)
            {
                current = priorityQueue.Dequeue();
                foreach (var kvp in current.Edges)
                {
                    double tenativeDistance = current.cumalativeDistance + kvp.Value;
                    if (tenativeDistance < kvp.Key.cumalativeDistance)
                    {
                        kvp.Key.cumalativeDistance = tenativeDistance;
                        kvp.Key.Parent = current;
                        if(!kvp.Key.wasVisited && !kvp.Key.wasQueued)
                        {
                            priorityQueue.Enqueue(kvp.Key, kvp.Key.cumalativeDistance);
                            kvp.Key.wasQueued = true;
                            kvp.Key.wasVisited = true;
                        }
                    }
                }
                current.wasVisited = true;
            }
            current = end;
            List<T> path = new List<T>();
            while (current != null)
            {
                path.Add(current.Value);
                current = current.Parent;
            }
            path.Reverse();
            return path;
        }
        public List<GridNode> AStarSearchAlgorithm(Vertex<GridNode> start, Vertex<GridNode> end)
        {
            PriorityQueue<Vertex<GridNode>, double> priorityQueue = new PriorityQueue<Vertex<GridNode>, double>();
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i].cumalativeDistance = double.MaxValue;
                vertices[i].finalDistance = double.MaxValue;
                vertices[i].Parent = null;
            }
            start.cumalativeDistance = 0;
            start.finalDistance = ManhattanHeuristic(start, end, 1);
            priorityQueue.Enqueue(start, start.finalDistance);
            Vertex<GridNode> current = start;
            while (current != end)
            {
                current = priorityQueue.Dequeue();
                foreach (var kvp in current.Edges)
                {
                    double tenativeDistance = current.cumalativeDistance + kvp.Value;
                    if (tenativeDistance < kvp.Key.cumalativeDistance)
                    {
                        kvp.Key.cumalativeDistance = tenativeDistance;
                        kvp.Key.Parent = current;
                        kvp.Key.finalDistance = kvp.Key.cumalativeDistance + ManhattanHeuristic(kvp.Key, end, 1);
                        if (!kvp.Key.wasVisited && !kvp.Key.wasQueued)
                        {
                            priorityQueue.Enqueue(kvp.Key, kvp.Key.cumalativeDistance);
                            kvp.Key.wasQueued = true;
                            kvp.Key.wasVisited = true;
                        }
                    }
                }
                current.wasVisited = true;
            }
            current = end;
            List<GridNode> path = new List<GridNode>();
            while (current != null)
            {
                path.Add(current.Value);
                current = current.Parent;
            }
            path.Reverse();
            return path;
        }
        private double ManhattanHeuristic(Vertex<GridNode> node, Vertex<GridNode> goal, double D)
        {
            double dx = Math.Abs(node.Value.X - goal.Value.X);
            double dy = Math.Abs(node.Value.Y - goal.Value.Y);
            return D * (dx + dy); 
        }
    }
}
