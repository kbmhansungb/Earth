using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ModelGenerator.Geometry
{
    public class Point
    {
        private Vector3 m_position;

        private List<Line> m_lines = new List<Line>();
        private List<Polygon> m_polygons = new List<Polygon>();

        public Vector3 Position { get=>m_position; set => m_position = value; }
        public ReadOnlyCollection<Line> Lines { get => m_lines.AsReadOnly(); }
        public ReadOnlyCollection<Polygon> Polygons { get=>m_polygons.AsReadOnly(); }

        public Point(Vector3 position)
        {
            this.m_position = position;
        }

        public void AddLine(Line line)
        {
            m_lines.Add(line);
        }

        public void RemoveLine(Line line)
        {
            m_lines.Remove(line);
        }

        public void AddPolygon(Polygon polygon)
        {
            m_polygons.Add(polygon);
        }

        public void RemovePolygon(Polygon polygon)
        {
            m_polygons.Remove(polygon);
        }
    }
} 