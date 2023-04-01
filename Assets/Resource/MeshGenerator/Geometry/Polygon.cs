using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MeshGenerator.Geometry
{
    public class Polygon
    {
        private List<Point> m_points = new List<Point>();
        private List<Line> m_lines = new List<Line>();

        public ReadOnlyCollection<Point> Points { get => m_points.AsReadOnly(); }
        public ReadOnlyCollection<Line> Lines { get => m_lines.AsReadOnly(); }

        /// <summary>
        /// 면을 생성합니다.
        /// </summary>
        /// <param name="lines">
        /// 마지막 선은 처음 시작한 점에서 끝나야 합니다. 
        /// 예를 들면 { A -> B -> C -> A }의 면의 선은 { A -> B } ... { C -> A }여야 합니다.
        /// </param>
        public Polygon(IEnumerable<Line> lines)
        {
            foreach(var line in lines)
            {
                m_points.Add(line.Begin);
                m_lines.Add(line);

                line.Begin.AddPolygon(this);
                line.SetPolygon(this);
            }
        }
    }
} 