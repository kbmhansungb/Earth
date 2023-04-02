using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
namespace ModelGenerator
{
    public static class Icosahedron
    {
        public static (List<Vector3> points, List<int> triangles) GetIcosahedron()
        {
            List<Vector3> points = GetIcosahedronPoints();
            List<int> triangles = GetIcosahedronTriangles();

            points = RotateTo(MakeNorthPole, points.AsReadOnly(), 0);

            return (points, triangles);
        }

        public static List<Vector3> GetIcosahedronPoints()
        {
            // Golden Ratio
            float t = (1f + Mathf.Sqrt(5f)) / 2f;

            List<Vector3> points = new List<Vector3> {
                new Vector3(-1,  t,  0).normalized,
                new Vector3( 1,  t,  0).normalized,
                new Vector3(-1, -t,  0).normalized,
                new Vector3( 1, -t,  0).normalized,
                new Vector3( 0, -1,  t).normalized,
                new Vector3( 0,  1,  t).normalized,
                new Vector3( 0, -1, -t).normalized,
                new Vector3( 0,  1, -t).normalized,
                new Vector3( t,  0, -1).normalized,
                new Vector3( t,  0,  1).normalized,
                new Vector3(-t,  0, -1).normalized,
                new Vector3(-t,  0,  1).normalized
            };

            return points;
        }

        public static List<int> GetIcosahedronTriangles()
        {
            List<int> triangles = new List<int>{
                0,  11, 5,
                0,  5,  1,
                0,  1,  7,
                0,  7,  10,
                0,  10, 11,
                1,  5,  9,
                5,  11, 4,
                11, 10, 2,
                10, 7,  6,
                7,  1,  8,
                3,  9,  4,
                3,  4,  2,
                3,  2,  6,
                3,  6,  8,
                3,  8,  9,
                4,  9,  5,
                2,  4,  11,
                6,  2,  10,
                8,  6,  7,
                9,  8,  1
            };

            return triangles;
        }

        public static List<Vector3> RotateTo(Func<Vector3, Quaternion> makeRotation, ReadOnlyCollection<Vector3> points, in int standardPointIndex)
        {
            Quaternion rotation = makeRotation(points[standardPointIndex]);

            // 각 정점에 회전 적용 후 반환합니다.
            List<Vector3> newPoints = new List<Vector3>(points.Count);
            for (int i = 0; i < points.Count; i++)
            {
                newPoints.Add(rotation * points[i]);
            }
            return newPoints;
        }

        public static Quaternion MakeNorthPole(Vector3 position)
        {
            return Quaternion.FromToRotation(position.normalized, Vector3.up);
        }
    }
}
