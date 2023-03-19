using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BM.MeshGenerator
{
    public static class IcosahedronMeshGenerator
    {
        public static void AddIcosahedron(this MeshGenerator generator)
        {
            int lastIndex = generator.Vertices.Count;
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
                lastIndex + 0,      lastIndex + 11,     lastIndex + 5,
                lastIndex + 0,      lastIndex + 5,      lastIndex + 1,
                lastIndex + 0,      lastIndex + 1,      lastIndex + 7,
                lastIndex + 0,      lastIndex + 7,      lastIndex + 10,
                lastIndex + 0,      lastIndex + 10,     lastIndex + 11,
                lastIndex + 1,      lastIndex + 5,      lastIndex + 9,
                lastIndex + 5,      lastIndex + 11,     lastIndex + 4,
                lastIndex + 11,     lastIndex + 10,     lastIndex + 2,
                lastIndex + 10,     lastIndex + 7,      lastIndex + 6,
                lastIndex + 7,      lastIndex + 1,      lastIndex + 8,
                lastIndex + 3,      lastIndex + 9,      lastIndex + 4,
                lastIndex + 3,      lastIndex + 4,      lastIndex + 2,
                lastIndex + 3,      lastIndex + 2,      lastIndex + 6,
                lastIndex + 3,      lastIndex + 6,      lastIndex + 8,
                lastIndex + 3,      lastIndex + 8,      lastIndex + 9,
                lastIndex + 4,      lastIndex + 9,      lastIndex + 5,
                lastIndex + 2,      lastIndex + 4,      lastIndex + 11,
                lastIndex + 6,      lastIndex + 2,      lastIndex + 10,
                lastIndex + 8,      lastIndex + 6,      lastIndex + 7,
                lastIndex + 9,      lastIndex + 8,      lastIndex + 1
            };

            generator.Vertices.AddRange(vertices);
            generator.Triangles.AddRange(triangles);
        }
    }
}
