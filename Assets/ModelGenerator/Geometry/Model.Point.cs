using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModelGenerator.Geometry
{
    public partial class Model : Shape
    {
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
    }
}
