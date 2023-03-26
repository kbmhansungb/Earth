using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace BM.MeshGenerator
{
    public class Model
    {
        private List<Vertex> m_vertices = new List<Vertex>();
        private List<Line> m_lines = new List<Line>();
        private List<Polygon> m_polygons = new List<Polygon>();

        public ReadOnlyCollection<Vertex> Vertices { get => m_vertices.AsReadOnly(); }
        public ReadOnlyCollection<Line> Lines { get => m_lines.AsReadOnly(); }
        public ReadOnlyCollection<Polygon> Polygons { get => m_polygons.AsReadOnly(); }

        public Vertex AddVertex(Vector3 position)
        {
            var newVertex = new Vertex(position);
            m_vertices.Add(newVertex);
            return newVertex;
        }

        public Line AddLine(Vertex Begin, Vertex End)
        {
            var newLine = new Line(Begin, End);

            m_lines.Add(newLine);
            Begin.Lines.Add(newLine);
            End.Lines.Add(newLine);

            return newLine;
        }

        public Polygon AddPolygon(IEnumerable<Line> lines)
        {
            var polygon = new Polygon(lines);
            m_polygons.Add(polygon);
            return polygon;
        }

        public Line GetLine(Vertex A, Vertex B)
        {
            foreach(var line in m_lines)
            {
                if (line.Begin == A && line.End == B)
                {
                    return line;
                }
                else if (line.Begin == B && line.End == A)
                {
                    return line.ReversedLine;
                }
            }

            return null;
        }
    }
} 