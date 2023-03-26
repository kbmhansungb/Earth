using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace BM.MeshGenerator
{
    public class Vertex
    {
        public Vector3 Position;

        public List<Line> Lines = new List<Line>();
        public List<Polygon> Polygons = new List<Polygon>();

        public Vertex(Vector3 position)
        {
            this.Position = position;
        }
    }
} 