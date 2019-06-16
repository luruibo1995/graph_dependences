using System;

using Graph;

namespace GraphTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Vertex vertex0 = new Vertex(Vertex.Kind.Project, "0");
            Vertex vertex1 = new Vertex(Vertex.Kind.dependencies, "1");
            Vertex vertex2 = new Vertex(Vertex.Kind.dependencies, "2");
            Vertex vertex3 = new Vertex(Vertex.Kind.dependencies, "3");
            Vertex vertex4 = new Vertex(Vertex.Kind.dependencies, "4");
            Vertex vertex5 = new Vertex(Vertex.Kind.dependencies, "5");
            Vertex vertex6 = new Vertex(Vertex.Kind.dependencies, "6");
            Digraph digraph_vertex = new Digraph();
            digraph_vertex.Add(vertex0, vertex1);
            digraph_vertex.Add(vertex0, vertex2);
            digraph_vertex.Add(vertex0, vertex3);
            digraph_vertex.Add(vertex2, vertex4);
            digraph_vertex.Add(vertex3, vertex4);
            digraph_vertex.Add(vertex4, vertex5);
            digraph_vertex.Add(vertex4, vertex6);
            digraph_vertex.Add(vertex6, vertex5);

            //digraph_vertex.addEdge(vertex1, vertex2);

            //foreach (Vertex vertex in vertex4.FromVertices)
            //{
            //    Console.WriteLine(vertex.Id);
            //}

            //foreach (var list in WaysbetweenTwoVertices.GetWaysBetweenTwoVertices(vertex0, vertex5))
            //{
            //    for(int i = 0; i < list.Count; i++)
            //    {
            //        if (i != list.Count - 1)
            //            Console.Write(list[i].Id + "->");
            //        else
            //            Console.Write(list[i].Id);
            //    }
            //    Console.WriteLine();
            //}


            //foreach(var vertex in AllDependences.GetVertexAllDependences(vertex0))
            //{
            //    Console.WriteLine(vertex.Id);
            //}

            foreach (var vertex in digraph_vertex.TopologicalSortUpToDown())
                Console.WriteLine(vertex.Id);

        }
    }
}
