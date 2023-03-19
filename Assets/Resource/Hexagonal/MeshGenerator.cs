using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BM.MeshGenerator
{
    public sealed class MeshGenerator
    {
    }

    public static class MeshGenerator_Extention
    { 
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