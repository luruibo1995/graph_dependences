using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;

namespace Graph
{ 
    public interface IVertex
    {
        string Id { get; set; }
    }

    public class Edge <T> 
    {
        public T Src
        {
            get;
            set;
        }

        public T Dest
        {
            get;
            set;
        }
        public Edge(T node1, T node2)
        {
            Src = node1;
            Dest = node2;
        }
    }
    public class Digraph
    {
        HashSet<Vertex> vertexs;
        HashSet<Edge<Vertex>> edges;
        Stack<Vertex> reversePostStack;
        Queue<Vertex> reversePostQueue;
        bool reverseOrNot;
        public int VertexCount
        {
            get;
            set;
        }

        public int EdgeCount
        {
            get;
            set;
        }

        public Digraph()
        {
            VertexCount = 0;
            EdgeCount = 0;
            vertexs = new HashSet<Vertex>();
            edges = new HashSet<Edge<Vertex>>();
            reverseOrNot = false;
        }

        public void Add(Vertex node1, params Vertex[] nodes)
        {
            reverseOrNot = false;
            if (nodes.Length == 0)
                AddVertex(node1);
            else if (nodes.Length == 1)
                AddEdge(node1, nodes[0]);
            else
                throw new Exception("error input");
        }
        public void AddVertex(Vertex node)
        {
            vertexs.Add(node);
            VertexCount++;
        }
        public void AddEdge( Vertex node1, Vertex node2)
        {
            EdgeCount++;
            if ( !vertexs.Contains(node1))
                vertexs.Add(node1);
            if ( !vertexs.Contains(node2))
                vertexs.Add(node2);
            edges.Add(new Edge<Vertex>(node1, node2));
            node1.AddDestVertices(node2);
            node2.AddFromVertices(node1);
        }

        public void DeleteVertex(Vertex node)
        {
            if (!vertexs.Contains(node))
                throw new Exception("error node");
            vertexs.Remove(node);
            VertexCount--;
            foreach (Vertex vertex in node.DestVertices)
                vertex.FromVertices.Remove(node);
            foreach (Vertex vertex in node.FromVertices)
                vertex.DestVertices.Remove(node);
        }
        public void DeleteEdge(Vertex node1, Vertex node2)
        {
            Edge<Vertex> edge = new Edge<Vertex>(node1, node2);
            if (!edges.Contains(edge))
                throw new Exception("error edge");
            EdgeCount--;
            edges.Remove(edge);
            node1.DestVertices.Remove(node2);
            node2.FromVertices.Remove(node1);
        }

        public void Delete(Vertex node1, params Vertex[] nodes)
        {
            reverseOrNot = false;
            if (nodes.Length == 0)
                DeleteVertex(node1);
            else if (nodes.Length == 1)
                DeleteEdge(node1, nodes[0]);
            else
                throw new Exception("error input");
        }

        private void DFS(Vertex vertex)
        {
            vertex.Visited = true;
            foreach(Vertex tempVertex in vertex.DestVertices)
            {
                if (!tempVertex.Visited)
                    DFS(tempVertex);
            }
            reversePostStack.Push(vertex);

        }

        private void ReverseStackToQueue<T>(ref Stack<T> stack, ref Queue<T> queue)
        {
            if(stack.Count > 0)
            {
                T temp = stack.Pop();
                ReverseStackToQueue(ref stack, ref queue);
                queue.Enqueue(temp);
            }
        }
//the output order is the arrow from up to down//
//a
//|
//v
//b
        public Stack<Vertex> TopologicalSortUpToDown()
        {
            reversePostStack = new Stack<Vertex>();
            reverseOrNot = true;
            foreach(Vertex vertex in vertexs)
            {
                if (!vertex.Visited)
                    DFS(vertex);
            }

            return reversePostStack;
        }

        public Queue<Vertex> TopologicalSortDownToUp()
        {
            reversePostQueue = new Queue<Vertex>();
            if (!reverseOrNot)
            {
                TopologicalSortUpToDown();
            }
            ReverseStackToQueue<Vertex>(ref reversePostStack, ref reversePostQueue);
            return reversePostQueue;

        }

    }
    public class Vertex : IVertex
    {
        public enum Kind
        {
            Project,
            dependencies
        };
        static int vertex_count = 0;
        private int vertex_id;

        public bool Visited { get; set; }

        public string Id { get; set; }

        private Kind vertex_kind;
        private string id;
       
        public Vertex(Kind kind, string id)
        {
            Id = id;
            DestVertices = new HashSet<Vertex>();
            FromVertices = new HashSet<Vertex>();
            vertex_kind = kind;
            vertex_id = vertex_count;
            vertex_count++;
            Visited = false;
        }
        public int Vertex_id
        {
            get
            {
                return vertex_id;
            }
        }

        public void AddDestVertices(Vertex dest)
        {
            this.DestVertices.Add(dest);
        }
        public void AddFromVertices(Vertex from)
        {
            FromVertices.Add(from);
        }

        public HashSet<Vertex> DestVertices
        { get; }
        public HashSet<Vertex> FromVertices
        { get; }
    }



    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //Vertex vertex0 = new Vertex(Vertex.Kind.Project, "0");
            //Vertex vertex1 = new Vertex(Vertex.Kind.dependencies, "1");
            //Vertex vertex2 = new Vertex(Vertex.Kind.dependencies, "2");
            //Vertex vertex3 = new Vertex(Vertex.Kind.dependencies, "3");
            //Vertex vertex4 = new Vertex(Vertex.Kind.dependencies, "4");
            //Vertex vertex5 = new Vertex(Vertex.Kind.dependencies, "5");
            //Digraph digraph_vertex = new Digraph();
            //digraph_vertex.Add(vertex0, vertex1);
            //digraph_vertex.Add(vertex0, vertex2);
            //digraph_vertex.Add(vertex0, vertex3);
            //digraph_vertex.Add(vertex2, vertex4);
            //digraph_vertex.Add(vertex3, vertex4);
            //digraph_vertex.Add(vertex4, vertex5);
            ////digraph_vertex.addEdge(vertex1, vertex2);

            //foreach (Vertex vertex in vertex4.FromVertices)
            //{
            //    Console.WriteLine(vertex.Id);
            //}


        }
    }
}
