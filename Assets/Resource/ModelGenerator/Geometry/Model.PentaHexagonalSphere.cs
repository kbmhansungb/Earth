using ModelGenerator.Geometry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UIElements;

namespace ModelGenerator.Geometry
{
    public partial class Model
    {
        /// <summary>
        /// 분할된 정이십면체를 이용하여 오각, 육각 구를 생성합니다.
        /// </summary>
        /// <param name="sourceModel"></param>
        /// <returns></returns>
        public Model CreatePentaHexagonalSphere()
        {
            Model pentaHexagonalSphere = new Model();

            // 기준이 되는 모델의 점을 복사합니다.
            Dictionary<Point, Point> pointMap = new Dictionary<Point, Point>();
            EachPoint(sourcePoint => {
                Point newPoint = pentaHexagonalSphere.AddPoint(sourcePoint.Position);
                pointMap.Add(sourcePoint, newPoint);
            });

            // 기준이 되는 삼각형의 면의 중심을 점으로 추가합니다.
            Dictionary<Polygon, Point> centriodMap = new Dictionary<Polygon, Point>();
            EachPolygon(sourcePolygon => {
                Point newPoint = pentaHexagonalSphere.AddPoint(sourcePolygon.GetCentroidPosition());
                centriodMap.Add(sourcePolygon, newPoint);
            });

            EachPoint(sourcePoint => {
                // 선을 중심으로 기존의 점과 선의 좌우에 있는 삼각형의 중심을 꼭지점으로 하는 삼각형을 추가합니다.
                foreach (var line in sourcePoint.Lines)
                {
                    var p1 = centriodMap[line.Left];
                    var p2 = centriodMap[line.Right];
                    var p3 = pointMap[sourcePoint];

                    pentaHexagonalSphere.AddPolygon(p1, p2, p3);
                }

                // 기준이 되는 모델의 점의 좌표를 기준이 되는 모델의 삼각형의 중심의 평균으로 합니다.
                Vector3 newPosition = Vector3.zero;
                foreach (var line in sourcePoint.Lines)
                {
                    newPosition += centriodMap[line.Left].Position;
                }
                newPosition /= sourcePoint.Lines.Count;
                pointMap[sourcePoint].Position = newPosition;
            });

            return pentaHexagonalSphere;
        }
    }
}
