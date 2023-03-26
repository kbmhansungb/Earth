using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace BM.MeshGenerator
{
    public class Polygon
    {
        private List<Vertex> m_vertices = new List<Vertex>();
        private List<Line> m_lines = new List<Line>();

        public ReadOnlyCollection<Vertex> Vertices { get => m_vertices.AsReadOnly(); }
        public ReadOnlyCollection<Line> Lines { get => m_lines.AsReadOnly(); }

        public Polygon(IEnumerable<Line> lines)
        {
            foreach(var line in lines)
            {
                m_vertices.Add(line.Begin);
                m_lines.Add(line);

                line.Begin.Polygons.Add(this);
                line.Right = this;
            }
        }
    }
} 