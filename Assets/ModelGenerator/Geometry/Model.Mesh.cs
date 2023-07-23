using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using ModelGenerator.Geometry;


namespace ModelGenerator.Geometry
{
    public partial class Model : Shape
    {
        public static Model CreateModelFromMesh(Mesh mesh)
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

        public Mesh CreateMesh()
        {
            // 테스트를 위한 간단한 모델
            List<Vector3> points = new List<Vector3>();
            List<int> triangles = new List<int>();

            int index = 0;
            foreach (var triangle in Polygons)
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
    }
} 