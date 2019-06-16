using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public static class WaysbetweenTwoVertices
    {
        public static  HashSet<List<Vertex>> GetWaysBetweenTwoVertices(Vertex node1, Vertex node2)
        {
            if (node1 == null || node2 == null)
                return null;
            HashSet<List<Vertex>> outputList = new HashSet<List<Vertex>>();
            List<Vertex> vertexList = new List<Vertex>();
            vertexList.Add(node1);
            GetWaysBetweenVerticesImpl(node1, node2,ref vertexList, ref outputList);
            return outputList;
        }

        private static void GetWaysBetweenVerticesImpl(Vertex node, Vertex destnation, ref List<Vertex> vertexList, ref HashSet<List<Vertex>> outputList)
        {
            if (node == null || destnation == null || vertexList == null)
                return;
            if(outputList == null)
                outputList = new HashSet<List<Vertex>>();
            if (node == destnation)
            {
                List<Vertex> storeList = new List<Vertex>(vertexList);
                outputList.Add(storeList);
                return;
            }
            foreach(Vertex vertex in node.DestVertices)
            {
                vertexList.Add(vertex);
                GetWaysBetweenVerticesImpl(vertex, destnation, ref vertexList, ref outputList);
                vertexList.Remove(vertex);
            }
        }
    }
}
