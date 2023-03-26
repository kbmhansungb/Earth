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

    public class Line
    {
        private bool m_isDefault;
        private Line m_reversedLine;

        // A -> B로 가는 라인과 B -> A로 가는 라인은 유사하지만, 왼쪽과 오른쪽 폴리곤이 반전됩니다.
        // 따라서 기본 상태가 아니면 반전된 라인입니다.
        private class Data
        {
            public Vertex Begin;
            public Vertex End;

            public Polygon Left;
            public Polygon Right;
        }
        private Data m_data = new Data();

        public Line ReversedLine { get => m_reversedLine; }
        public Vertex Begin { get => m_isDefault ? m_data.Begin : m_data.End; }
        public Vertex End { get => m_isDefault ? m_data.End : m_data.Begin; }
        public Polygon Left { 
            get => m_isDefault ? m_data.Left : m_data.Right; 
            set { 
                if (m_isDefault)
                    m_data.Left = value;
                else
                    m_data.Right = value;
            }
        }
        public Polygon Right { 
            get => m_isDefault ? m_data.Right : m_data.Left;
            set
            {
                if (m_isDefault)
                    m_data.Right = value;
                else
                    m_data.Left = value;
            }
        }

        private Line() { }

        public Line(Vertex Begin, Vertex End)
        {
            m_isDefault = false;

            m_data.Begin = Begin;
            m_data.End = End;

            m_reversedLine = new Line();
            m_reversedLine.m_isDefault = true;
            m_reversedLine.m_reversedLine = this;
            m_reversedLine.m_data = m_data;

            m_data.Begin.Lines.Add(this);
            m_data.End.Lines.Add(this);
        }
    }

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