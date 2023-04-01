using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

namespace MeshGenerator.Geometry
{
    public class Model
    {
        private List<Vertex> m_vertices = new List<Vertex>();
        private List<Line> m_lines = new List<Line>();
        private List<Polygon> m_polygons = new List<Polygon>();

        public ReadOnlyCollection<Vertex> Vertices { get => m_vertices.AsReadOnly(); }
        public ReadOnlyCollection<Line> Lines { get => m_lines.AsReadOnly(); }
        public ReadOnlyCollection<Polygon> Polygons { get => m_polygons.AsReadOnly(); }

        /*
         *  Vertex
         */

        // 모델에 버텍스를 추가합니다.
        public Vertex AddVertex(Vector3 position)
        {
            var newVertex = new Vertex(position);
            m_vertices.Add(newVertex);
            return newVertex;
        }

        // 모델에 버텍스들을 추가합니다.
        public List<Vertex> AddVertices(ReadOnlyCollection<Vector3> vertices)
        {
            var newVertices = new List<Vertex>(vertices.Count);
            for (int index = 0; index < vertices.Count; index++)
            {
                newVertices.Add(AddVertex(vertices[index]));
            }
            return newVertices;
        }

        /*
         *  Line
         */

        public Line AddLine(Vertex Begin, Vertex End)
        {
            Line newLine = new Line(Begin, End);
            m_lines.Add(newLine);
            return newLine;
        }

        public Line AddLine(Vector3 beginPosition, Vector3 endPosition, Func<Vector3, Vertex> AddVertex)
        {
            Vertex begin = AddVertex(beginPosition);
            Vertex end = AddVertex(endPosition);
            return AddLine(begin, end);
        }

        public Line GetLine(Vertex A, Vertex B)
        {
            foreach (var line in m_lines)
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

        /*
         *  Polygon
         */

        public Polygon AddPolygon(IEnumerable<Line> lines)
        {
            var polygon = new Polygon(lines);
            m_polygons.Add(polygon);
            return polygon;
        }

        public List<Polygon> AddPolygons(List<Vector3> verticesData, List<int> trianglesData)
        {
            List<Vertex> vertices = AddVertices(verticesData.AsReadOnly());
            
            List<Polygon> polygons = new List<Polygon>(trianglesData.Count / 3);
            for (int triangleIndex = 0; triangleIndex < trianglesData.Count; triangleIndex += 3)
            {
                Vertex Vertex1 = vertices[trianglesData[triangleIndex + 0]];
                Vertex Vertex2 = vertices[trianglesData[triangleIndex + 1]];
                Vertex Vertex3 = vertices[trianglesData[triangleIndex + 2]];

                Line line1 = AddLine(Vertex1, Vertex2);
                Line line2 = AddLine(Vertex2, Vertex3);
                Line line3 = AddLine(Vertex3, Vertex1);

                Polygon polygon = AddPolygon(new Line[] { line1, line2, line3 });
            }
            return polygons;
        }
    }
} 