using ModelGenerator.Geometry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UIElements;

namespace ModelGenerator
{
    public static class IcosahedronExtention
    {
        /// <summary>
        /// 모델에 정이십면체를 추가합니다.
        /// </summary>
        /// <param name="model"></param>
        public static void AddIcosahedron(this Model model)
        {
            List<Vector3> positions = GetIcosahedronPositions();
            List<int> triangles = GetIcosahedronTriangles();

            model.AddPolygons(positions, triangles);
        }

        /// <summary>
        /// 해당 포지션을 북극점으로 회전시키는 쿼터니언을 구합니다.
        /// </summary>
        public static Quaternion MakeNorthPoleQuaternion(this Model model, int positionIndex)
        {
            return MakeNorthPoleQuaternion(model.Points[positionIndex].Position);
        }


        public static List<Vector3> GetIcosahedronPositions()
        {
            // Golden Ratio
            float t = (1f + Mathf.Sqrt(5f)) / 2f;

            List<Vector3> points = new List<Vector3> {
                new Vector3(-1,  t,  0).normalized,
                new Vector3( 1,  t,  0).normalized,
                new Vector3(-1, -t,  0).normalized,
                new Vector3( 1, -t,  0).normalized,
                new Vector3( 0, -1,  t).normalized,
                new Vector3( 0,  1,  t).normalized,
                new Vector3( 0, -1, -t).normalized,
                new Vector3( 0,  1, -t).normalized,
                new Vector3( t,  0, -1).normalized,
                new Vector3( t,  0,  1).normalized,
                new Vector3(-t,  0, -1).normalized,
                new Vector3(-t,  0,  1).normalized
            };

            return points;
        }

        public static List<int> GetIcosahedronTriangles()
        {
            List<int> triangles = new List<int>{
                0,  11, 5,
                0,  5,  1,
                0,  1,  7,
                0,  7,  10,
                0,  10, 11,
                1,  5,  9,
                5,  11, 4,
                11, 10, 2,
                10, 7,  6,
                7,  1,  8,
                3,  9,  4,
                3,  4,  2,
                3,  2,  6,
                3,  6,  8,
                3,  8,  9,
                4,  9,  5,
                2,  4,  11,
                6,  2,  10,
                8,  6,  7,
                9,  8,  1
            };

            return triangles;
        }

        public static Quaternion MakeNorthPoleQuaternion(Vector3 position)
        {
            return Quaternion.FromToRotation(position.normalized, Vector3.up);
        }
    }

    public class IcosahedronGenerator : ModelGenerator
    {
        /// <summary>
        /// 모델을 생성합니다. 그리고 정이십면체를 생성한 후,
        /// 0번 버택스가 극점이 되도록 회전시킵니다.
        /// </summary>
        /// <returns></returns>
        public Model CreateIcosahedron()
        {
            Model model = new Model();

            model.AddIcosahedron();
            model.Rotate( model.MakeNorthPoleQuaternion(0) );

            return model;
        }

        /// <summary>
        /// 분할된 정이십면체를 이용하여 오각, 육각 구를 생성합니다.
        /// </summary>
        /// <param name="sourceModel"></param>
        /// <returns></returns>
        public Model CreatePentaHexagonalSphere(Model sourceModel)
        {
            Model pentaHexagonalSphere = new Model();

            // 기준이 되는 모델의 점을 복사합니다.
            Dictionary<Point, Point> pointMap = new Dictionary<Point, Point>();
            sourceModel.EachPoint(sourcePoint => {
                Point newPoint = pentaHexagonalSphere.AddPoint(sourcePoint.Position);
                pointMap.Add(sourcePoint, newPoint);
            });

            // 기준이 되는 삼각형의 면의 중심을 점으로 추가합니다.
            Dictionary<Polygon, Point> centriodMap = new Dictionary<Polygon, Point>();
            sourceModel.EachPolygon(sourcePolygon => {
                Point newPoint = pentaHexagonalSphere.AddPoint(sourcePolygon.GetCentroidPosition());
                centriodMap.Add(sourcePolygon, newPoint);
            });

            sourceModel.EachPoint(sourcePoint => { 
                // 선을 중심으로 기존의 점과 선의 좌우에 있는 삼각형의 중심을 꼭지점으로 하는 삼각형을 추가합니다.
                foreach(var line in sourcePoint.Lines)
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
