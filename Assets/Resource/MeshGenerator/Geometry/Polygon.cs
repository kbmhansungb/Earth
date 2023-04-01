using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MeshGenerator.Geometry
{
    public class Polygon
    {
        private List<Vertex> m_vertices = new List<Vertex>();
        private List<Line> m_lines = new List<Line>();

        public ReadOnlyCollection<Vertex> Vertices { get => m_vertices.AsReadOnly(); }
        public ReadOnlyCollection<Line> Lines { get => m_lines.AsReadOnly(); }

        /// <summary>
        /// 폴리곤을 생성합니다.
        /// </summary>
        /// <param name="lines">
        /// 마지막 선은 처음 시작한 버택스를 끝나는 버택스 여야 합니다. 
        /// 예를 들면 { A -> B -> C -> A }의 폴리곤의 선에서 Lines { A -> B } ... { C -> A }와 같습니다.
        /// </param>
        public Polygon(IEnumerable<Line> lines)
        {
            foreach(var line in lines)
            {
                m_vertices.Add(line.Begin);
                m_lines.Add(line);

                line.Begin.AddPolygon(this);
                line.SetPolygon(this);
            }
        }
    }
} 