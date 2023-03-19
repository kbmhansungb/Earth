using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BM.MeshGenerator
{
    public static class MeshGenerator_IcosahedronExtention
    {
        public static Mesh CreateIcosahedron(this MeshGenerator generator)
        {
            var mesh = new Mesh();

            // Golden Ratio
            float t = (1f + Mathf.Sqrt(5f)) / 2f;

            Vector3[] vertices = {
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

            // 회전 각도 계산
            float angleX = Mathf.Acos(Vector3.Dot(Vector3.right, vertices[0])) * Mathf.Rad2Deg;
            float angleZ = Mathf.Atan2(vertices[0].z, vertices[0].x) * Mathf.Rad2Deg;

            // Quaternion을 사용하여 회전 행렬 생성
            Quaternion rotation = Quaternion.Euler(angleX, 0, -angleZ);

            // 각 정점에 회전 적용
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = rotation * vertices[i];
            }

            int[] triangles = {
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

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            return mesh;
        }
    }
}
