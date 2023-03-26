using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace BM.MeshGenerator
{
    public sealed class MeshGenerator
    {
    }

    public static class MeshGenerator_Extention
    {
        public static Model MakeModel(this MeshGenerator generator, Mesh mesh)
        {
            var model = new Model();

            foreach(var vertex in mesh.vertices)
            {
                model.AddVertex(vertex);
            }

            for (int triangleIndex = 0; triangleIndex < mesh.triangles.Length / 3; triangleIndex++)
            {
                Vertex vertex1 = model.Vertices[mesh.triangles[triangleIndex * 3 + 0]];
                Vertex vertex2 = model.Vertices[mesh.triangles[triangleIndex * 3 + 1]];
                Vertex vertex3 = model.Vertices[mesh.triangles[triangleIndex * 3 + 2]];

                Line lineElum1 = model.GetLine(vertex1, vertex2) ?? model.AddLine(vertex1, vertex2);
                Line lineElum2 = model.GetLine(vertex2, vertex3) ?? model.AddLine(vertex2, vertex3);
                Line lineElum3 = model.GetLine(vertex3, vertex1) ?? model.AddLine(vertex3, vertex1);

                model.AddPolygon(new Line[] { lineElum1, lineElum2, lineElum3 });
            }

            return model;
        }

        public static Mesh MakeMesh(this MeshGenerator generator, Model model)
        {
            // 테스트를 위한 간단한 모델
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            int index = 0;
            foreach (var triangle in model.Polygons)
                foreach(var vertex in triangle.Vertices)
                {
                    vertices.Add(vertex.Position);
                    triangles.Add(index++);
                }

            var mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            return mesh;
        }

        public static void normalizeSphere(this Mesh mesh, Vector3 origin, float length = 1.0f)
        {
            var vertices = mesh.vertices;
            
            for(int index = 0; index < vertices.Length; index++)
            {
                vertices[index] = origin + (vertices[index] - origin).normalized * length;
            }

            mesh.vertices = vertices;
        }
    }
} 