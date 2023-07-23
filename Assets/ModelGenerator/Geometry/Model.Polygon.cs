using Mono.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModelGenerator.Geometry
{
    public partial class Model : Shape
    {
        public Polygon AddPolygon(Point p1, Point p2, Point p3, params Point[] points)
        {
            List<Point> newPoints = new List<Point>();
            newPoints.Add(p1);
            newPoints.Add(p2);
            newPoints.Add(p3);
            newPoints.AddRange(points);

            List<Line> newLines = new List<Line>();
            for (int index = 0; index < newPoints.Count; index++)
            {
                Point begin = newPoints[index];
                Point end = newPoints[(index + 1) % newPoints.Count];
                newLines.Add(AddUniqueLine(begin, end));
            }

            return AddPolygon(newLines);
        }

        public Polygon AddPolygon(IEnumerable<Line> lines)
        {
            var polygon = new Polygon(lines);
            m_polygons.Add(polygon);
            return polygon;
        }

        public List<Polygon> AddPolygons(List<Vector3> pointsData, List<int> trianglesData)
        {
            List<Point> points = AddPoints(pointsData.AsReadOnly());

            List<Polygon> polygons = new List<Polygon>(trianglesData.Count / 3);
            for (int triangleIndex = 0; triangleIndex < trianglesData.Count; triangleIndex += 3)
            {
                Point Point1 = points[trianglesData[triangleIndex + 0]];
                Point Point2 = points[trianglesData[triangleIndex + 1]];
                Point Point3 = points[trianglesData[triangleIndex + 2]];

                Line line1 = AddLine(Point1, Point2);
                Line line2 = AddLine(Point2, Point3);
                Line line3 = AddLine(Point3, Point1);

                Polygon polygon = AddPolygon(new Line[] { line1, line2, line3 });
            }
            return polygons;
        }

        /// <summary>
        /// 각 면에 대해 지정된 작업을 수행합니다.
        /// </summary>
        public void EachPolygon(Action<Polygon> modifyPolygon)
        {
            m_polygons.ForEach(modifyPolygon);
        }
    }
}
