using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using ModelGenerator.Geometry;

namespace ModelGenerator
{
    public class ModelGenerator
    {
    }

    public static class ModelGeneratorExtention
    {
        public static Model MakeModel(this ModelGenerator generator, Mesh mesh)
        {
            var model = new Model();

            foreach(var point in mesh.vertices)
            {
                model.AddPoint(point);
            }

            for (int triangleIndex = 0; triangleIndex < mesh.triangles.Length / 3; triangleIndex++)
            {
                Point point1 = model.Points[mesh.triangles[triangleIndex * 3 + 0]];
                Point point2 = model.Points[mesh.triangles[triangleIndex * 3 + 1]];
                Point point3 = model.Points[mesh.triangles[triangleIndex * 3 + 2]];

                Line lineElum1 = model.GetLine(point1, point2) ?? model.AddLine(point1, point2);
                Line lineElum2 = model.GetLine(point2, point3) ?? model.AddLine(point2, point3);
                Line lineElum3 = model.GetLine(point3, point1) ?? model.AddLine(point3, point1);

                model.AddPolygon(new Line[] { lineElum1, lineElum2, lineElum3 });
            }

            return model;
        }

        public static Mesh MakeMesh(this ModelGenerator generator, Model model)
        {
            // 테스트를 위한 간단한 모델
            List<Vector3> points = new List<Vector3>();
            List<int> triangles = new List<int>();

            int index = 0;
            foreach (var triangle in model.Polygons)
                foreach(var point in triangle.Points)
                {
                    points.Add(point.Position);
                    triangles.Add(index++);
                }

            var mesh = new Mesh();
            mesh.vertices = points.ToArray();
            mesh.triangles = triangles.ToArray();
            return mesh;
        }

        public static void normalizeSphere(this Mesh mesh, Vector3 origin, float length = 1.0f)
        {
            var points = mesh.vertices;
            
            for(int index = 0; index < points.Length; index++)
            {
                points[index] = origin + (points[index] - origin).normalized * length;
            }

            mesh.vertices = points;
        }
    }
} 