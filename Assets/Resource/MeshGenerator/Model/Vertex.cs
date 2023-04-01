using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MeshGenerator.Model
{
    public class Vertex
    {
        private Vector3 m_position;

        internal List<Line> m_lines = new List<Line>();
        internal List<Polygon> m_polygons = new List<Polygon>();

        public Vector3 Position { get=>m_position; set => m_position = value; }
        public ReadOnlyCollection<Line> Lines { get => m_lines.AsReadOnly(); }
        public ReadOnlyCollection<Polygon> Polygons { get=>m_polygons.AsReadOnly(); }

        public Vertex(Vector3 position)
        {
            this.m_position = position;
        }
    }
} 