using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BM.MeshGenerator
{
    public sealed class MeshGenerator
    {
        public List<Vector3> Vertices = new List<Vector3>();
        public List<Vector2> UVs = new List<Vector2>();
        public List<Vector3> Normals = new List<Vector3>();
        public List<int> Triangles = new List<int>();

        public Mesh Generate()
        {
            var mesh = new Mesh();

            mesh.SetVertices(Vertices);
            mesh.SetUVs(0, UVs);
            mesh.SetNormals(Normals);
            mesh.SetTriangles(Triangles, 0);

            return mesh;
        }
    }
}