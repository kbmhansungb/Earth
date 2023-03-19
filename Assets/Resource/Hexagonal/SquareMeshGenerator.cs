using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BM.MeshGenerator
{
    public static class Icoshadron
    {
        public static void AddSquare(this MeshGenerator generator, float size = 1.0f, Vector3 offset = new Vector3(), Vector3 rotation = new Vector3())
        {
            int lastIndex = generator.Vertices.Count;

            Quaternion quat = Quaternion.Euler(rotation);
            Vector3 LD = quat * (new Vector3(0,     0,      0) + offset);
            Vector3 LT = quat * (new Vector3(0,     size,   0) + offset);
            Vector3 RD = quat * (new Vector3(size,  0,      0) + offset);
            Vector3 RT = quat * (new Vector3(size,  size,   0) + offset);

            generator.Vertices.Add(LD);
            generator.Vertices.Add(LT);
            generator.Vertices.Add(RD);
            generator.Vertices.Add(RT);

            generator.Normals.Add(quat * -Vector3.forward);
            generator.Normals.Add(quat * -Vector3.forward);
            generator.Normals.Add(quat * -Vector3.forward);
            generator.Normals.Add(quat * -Vector3.forward);

            generator.UVs.Add(new Vector2(0, 0));
            generator.UVs.Add(new Vector2(0, 1));
            generator.UVs.Add(new Vector2(1, 0));
            generator.UVs.Add(new Vector2(1, 1));

            generator.Triangles.Add(lastIndex + 0);
            generator.Triangles.Add(lastIndex + 1);
            generator.Triangles.Add(lastIndex + 2);
            generator.Triangles.Add(lastIndex + 2);
            generator.Triangles.Add(lastIndex + 1);
            generator.Triangles.Add(lastIndex + 3);
        }
    }
}