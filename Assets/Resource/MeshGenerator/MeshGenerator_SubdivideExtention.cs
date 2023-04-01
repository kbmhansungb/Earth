using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    public static class MeshGenerator_SubdivideExtention
    {
        public static Mesh SubdivideMesh(this MeshGenerator generator, Mesh mesh)
        {
            Vector3[] originalPoints = mesh.vertices;
            int[] originalTriangles = mesh.triangles;

            List<Vector3> newPoints = new List<Vector3>(originalPoints);
            List<int> newTriangles = new List<int>();

            for (int i = 0; i < originalTriangles.Length; i += 3)
            {
                SubdivideTriangle(ref newPoints, ref newTriangles, originalTriangles[i], originalTriangles[i + 1], originalTriangles[i + 2]);
            }

            Mesh newMesh = new Mesh();
            newMesh.vertices = newPoints.ToArray();
            newMesh.triangles = newTriangles.ToArray();
            newMesh.RecalculateNormals();

            return newMesh;
        }

        private static void SubdivideTriangle(ref List<Vector3> points, ref List<int> triangles, int a, int b, int c)
        {
            Vector3 ab = (points[a] + points[b]) * 0.5f;
            Vector3 bc = (points[b] + points[c]) * 0.5f;
            Vector3 ca = (points[c] + points[a]) * 0.5f;

            int abIndex = points.Count;
            points.Add(ab);
            int bcIndex = points.Count;
            points.Add(bc);
            int caIndex = points.Count;
            points.Add(ca);

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
