using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BM.MeshGenerator
{
    public class MeshGenerator
    {
        public virtual Mesh Generate()
        {
            return new Mesh();
        }
    }
}