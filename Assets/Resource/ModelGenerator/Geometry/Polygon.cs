using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;

namespace ModelGenerator.Geometry
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

        /// <summary>
        /// 각 점의 평균을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetCentroidPosition()
        {
            Vector3 position = Vector3.zero;
            m_points.ForEach(p => position += p.Position);
            return position / m_points.Count;
        }

        /// <summary>
        /// 삼각형을 분할하는 각각의 삼각형에 대해서 액션을 실행합니다.
        /// </summary>
        /// <param name="action"></param>
        public void ForEachSubTriangle(Action<WeightedPointSet, WeightedPointSet, WeightedPointSet> action)
        {
            Assert.IsTrue(Points.Count == 3, "When the shape of a face is a triangle, we can only obtain sub-triangles.");

            Point point1 = Points[0];
            Point point2 = Points[1];
            Point point3 = Points[2];

            WeightedPointSet wPoint1 = new WeightedPointSet((point1, 1));
            WeightedPointSet wPoint12 = new WeightedPointSet((point1, 0.5f), (point2, 0.5f));
            WeightedPointSet wPoint2 = new WeightedPointSet((point2, 1));
            WeightedPointSet wPoint23 = new WeightedPointSet((point2, 0.5f), (point3, 0.5f));
            WeightedPointSet wPoint3 = new WeightedPointSet((point3, 1));
            WeightedPointSet wPoint31 = new WeightedPointSet((point3, 0.5f), (point1, 0.5f));

            action.Invoke(wPoint1, wPoint12, wPoint31);
            action.Invoke(wPoint12, wPoint2, wPoint23);
            action.Invoke(wPoint31, wPoint23, wPoint3);
            action.Invoke(wPoint31, wPoint12, wPoint23);
        }
    }
} 