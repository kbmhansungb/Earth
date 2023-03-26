using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace BM.MeshGenerator
{
    public sealed class MeshGenerator
    {
    }

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
        // 기본 상태가 아니면 반전된 라인입니다.
        private bool m_isDefault;
        private Line m_reversedLine;

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

    public static class MeshGenerator_Extention
    {
        public static Model MakeModel(this MeshGenerator generator, Mesh mesh)
        {
            var model = new Model();

            foreach(var vertex in mesh.vertices)
            {
                model.AddVertex(vertex);
            }

            for (int triangleIndex = 0; triangleIndex < mesh.triangles.Length / 3; triangleIndex++)
            {
                Vertex vertex1 = model.Vertices[mesh.triangles[triangleIndex * 3 + 0]];
                Vertex vertex2 = model.Vertices[mesh.triangles[triangleIndex * 3 + 1]];
                Vertex vertex3 = model.Vertices[mesh.triangles[triangleIndex * 3 + 2]];

                Line lineElum1 = model.GetLine(vertex1, vertex2) ?? model.AddLine(vertex1, vertex2);
                Line lineElum2 = model.GetLine(vertex2, vertex3) ?? model.AddLine(vertex2, vertex3);
                Line lineElum3 = model.GetLine(vertex3, vertex1) ?? model.AddLine(vertex3, vertex1);

                model.AddPolygon(new Line[] { lineElum1, lineElum2, lineElum3 });
            }

            return model;
        }

        public static Mesh MakeMesh(this MeshGenerator generator, Model model)
        {
            // 테스트를 위한 간단한 모델
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            int index = 0;
            foreach (var triangle in model.Polygons)
                foreach(var vertex in triangle.Vertices)
                {
                    vertices.Add(vertex.Position);
                    triangles.Add(index++);
                }

            var mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            return mesh;
        }

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