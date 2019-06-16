using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public static class AllDependences
    {
        public static HashSet<Vertex> GetVertexAllDependences(Vertex vertex)
        {
            if (vertex == null)
                return null;
            HashSet<Vertex> hashSetVertices = new HashSet<Vertex>();
            GetVertexAllDependencesImpl(vertex, ref hashSetVertices);
            return hashSetVertices;
        }

        private static void GetVertexAllDependencesImpl(Vertex vertex, ref HashSet<Vertex> hashSetVertices)
        {

            if (vertex == null)
                return;
            if (hashSetVertices == null)
                hashSetVertices = new HashSet<Vertex>();
            foreach(Vertex tempvertex in vertex.DestVertices)
            {
                hashSetVertices.Add(tempvertex);
                GetVertexAllDependencesImpl(tempvertex, ref hashSetVertices);
            }
        }
    }
}
