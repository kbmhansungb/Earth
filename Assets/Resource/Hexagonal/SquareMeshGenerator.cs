using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BM.MeshGenerator
{
    public static class SquareMeshGenerator
    {
        public static void AddSquare(this MeshGenerator generator)
        {
            generator.Vertices.Add(new Vector3(0, 0, 0));
            generator.Vertices.Add(new Vector3(0, 1, 0));
            generator.Vertices.Add(new Vector3(1, 0, 0));
            generator.Vertices.Add(new Vector3(1, 1, 0));

            generator.UVs.Add(new Vector2(0, 0));
            generator.UVs.Add(new Vector2(0, 1));
            generator.UVs.Add(new Vector2(1, 0));
            generator.UVs.Add(new Vector2(1, 1));

            generator.Normals.Add(-Vector3.forward);
            generator.Normals.Add(-Vector3.forward);
            generator.Normals.Add(-Vector3.forward);
            generator.Normals.Add(-Vector3.forward);

            generator.Triangles.Add(0);
            generator.Triangles.Add(1);
            generator.Triangles.Add(2);
            generator.Triangles.Add(2);
            generator.Triangles.Add(1);
            generator.Triangles.Add(3);
        }
    }
}