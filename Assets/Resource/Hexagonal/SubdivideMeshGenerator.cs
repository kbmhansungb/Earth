using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BM.MeshGenerator
{
    public static class SubdivideMeshGenerator
    {
        public static Mesh SubdivideMesh(this MeshGenerator generator, Mesh mesh)
        {
            Vector3[] originalVertices = mesh.vertices;
            int[] originalTriangles = mesh.triangles;

            List<Vector3> newVertices = new List<Vector3>(originalVertices);
            List<int> newTriangles = new List<int>();

            for (int i = 0; i < originalTriangles.Length; i += 3)
            {
                SubdivideTriangle(ref newVertices, ref newTriangles, originalTriangles[i], originalTriangles[i + 1], originalTriangles[i + 2]);
            }

            Mesh newMesh = new Mesh();
            newMesh.vertices = newVertices.ToArray();
            newMesh.triangles = newTriangles.ToArray();
            newMesh.RecalculateNormals();

            return newMesh;
        }

        private static void SubdivideTriangle(ref List<Vector3> vertices, ref List<int> triangles, int a, int b, int c)
        {
            Vector3 ab = (vertices[a] + vertices[b]) * 0.5f;
            Vector3 bc = (vertices[b] + vertices[c]) * 0.5f;
            Vector3 ca = (vertices[c] + vertices[a]) * 0.5f;

            int abIndex = vertices.Count;
            vertices.Add(ab);
            int bcIndex = vertices.Count;
            vertices.Add(bc);
            int caIndex = vertices.Count;
            vertices.Add(ca);

            triangles.Add(a);
            triangles.Add(abIndex);
            triangles.Add(caIndex);

            triangles.Add(abIndex);
            triangles.Add(b);
            triangles.Add(bcIndex);

            triangles.Add(caIndex);
            triangles.Add(bcIndex);
            triangles.Add(c);

            triangles.Add(abIndex);
            triangles.Add(bcIndex);
            triangles.Add(caIndex);
        }
    }
}
