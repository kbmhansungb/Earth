using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
namespace BM.MeshGenerator
{
    public static class Icosahedron
    {
        public static (List<Vector3>, List<int>) GetIcosahedron()
        {
            List<Vector3> vertices = GetIcosahedronVertices();
            List<int> triangles = GetIcosahedronTriangles();

            // 정이십면체의 꼭지점이 하늘을 바라보도록 수정합니다.
            vertices = MakeNorthPole(vertices.AsReadOnly());

            return (vertices, triangles);
        }

        public static List<Vector3> GetIcosahedronVertices()
        {
            // Golden Ratio
            float t = (1f + Mathf.Sqrt(5f)) / 2f;

            List<Vector3> vertices = new List<Vector3> {
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

            return vertices;
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

        public static List<Vector3> MakeNorthPole(ReadOnlyCollection<Vector3> vertices, in int standardVertexIndex = 0)
        {
            List<Vector3> newVertices = new List<Vector3>(vertices.Count);

            // 회전 각도 계산
            float angleX = Mathf.Acos(Vector3.Dot(Vector3.right, vertices[standardVertexIndex])) * Mathf.Rad2Deg;
            float angleZ = Mathf.Atan2(vertices[standardVertexIndex].z, vertices[standardVertexIndex].x) * Mathf.Rad2Deg;

            // Quaternion을 사용하여 회전 행렬 생성
            Quaternion rotation = Quaternion.Euler(angleX, 0, -angleZ);

            // 각 정점에 회전 적용
            for (int i = 0; i < vertices.Count; i++)
            {
                newVertices[i] = rotation * vertices[i];
            }

            return newVertices;
        }
    }
}
