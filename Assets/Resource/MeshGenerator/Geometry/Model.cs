using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

namespace ModelGenerator.Geometry
{
    public class Model
    {
        private List<Point> m_points = new List<Point>();
        private List<Line> m_lines = new List<Line>();
        private List<Polygon> m_polygons = new List<Polygon>();

        public ReadOnlyCollection<Point> Points { get => m_points.AsReadOnly(); }
        public ReadOnlyCollection<Line> Lines { get => m_lines.AsReadOnly(); }
        public ReadOnlyCollection<Polygon> Polygons { get => m_polygons.AsReadOnly(); }

        /*
         *  Point
         */

        /// <summary>
        /// 모델에 점을 추가합니다.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public void AddPoint(Point point)
        {
            m_points.Add(point);
        }

        /// <summary>
        /// 모델에 점을 추가합니다.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Point AddPoint(Vector3 position)
        {
            var newPoint = new Point(position);
            AddPoint(newPoint);
            return newPoint;
        }

        /// <summary>
        /// 모델에 점을 추가합니다.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public List<Point> AddPoints(ReadOnlyCollection<Vector3> points)
        {
            var newPoints = new List<Point>(points.Count);
            for (int index = 0; index < points.Count; index++)
            {
                newPoints.Add(AddPoint(points[index]));
            }
            return newPoints;
        }

        /*
         *  Line
         */

        public Line AddLine(Point Begin, Point End)
        {
            Line newLine = new Line(Begin, End);
            m_lines.Add(newLine);
            return newLine;
        }

        public Line AddLine(Vector3 beginPosition, Vector3 endPosition, Func<Vector3, Point> AddPoint)
        {
            Point begin = AddPoint(beginPosition);
            Point end = AddPoint(endPosition);
            return AddLine(begin, end);
        }

        public Line GetLine(Point A, Point B)
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

        /*
         * 
         */

        /// <summary>
        /// 모델을 회전시킵니다.
        /// </summary>
        /// <param name="quaternion">RotateEachPoint의 반환값으로 전달됩니다.</param>
        public void Rotate(Quaternion quaternion)
        {
            RotateEachPoint((point) => { return quaternion; });
        }

        /// <summary>
        /// 각각의 점에 회전을 구하여 적용시킵니다.
        /// </summary>
        /// <param name="getQuaternion"></param>
        public void RotateEachPoint(Func<Point, Quaternion> getQuaternionFromPoint)
        {
            foreach (var point in m_points)
            {
                point.Position = getQuaternionFromPoint(point) * point.Position;
            }
        }
    }
} 