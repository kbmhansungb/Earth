using Codice.Client.BaseCommands;
using ModelGenerator.Geometry;
using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace ModelGenerator
{
    public static class SubdivisionExtention
    {
        public static void ForEachPointBySpliteLine(this Model model, int spliteCount, Action<WeightedPointSet> action)
        {
            foreach(var line in model.Lines)
            {
                for (int index = 0; index <= spliteCount + 1; index++)
                {
                    float weight = (float) index / (spliteCount + 1);
                    action.Invoke( new WeightedPointSet((line.Begin, 1 - weight), (line.End, weight)) );
                }
            }
        }

        public static void ForEachSubTriangle(this Polygon polygon, Action<WeightedPointSet, WeightedPointSet, WeightedPointSet> action)
        {
            Assert.IsTrue(polygon.Points.Count == 3, "When the shape of a face is a triangle, we can only obtain sub-triangles.");

            Point point1 = polygon.Points[0];
            Point point2 = polygon.Points[1];
            Point point3 = polygon.Points[2];

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

    public class SubdivisionGenerator : ModelGenerator
    {
        public Model CreateSubdivision(Model sourceModel)
        {
            Dictionary<WeightedPointSet, Point> newPointDictionary = new Dictionary<WeightedPointSet, Point>();

            Model subdividedModel = new Model();

            int spliteCount = 1;

            sourceModel.ForEachPointBySpliteLine(spliteCount, weightedPointSet => {
                // 점이 없을 경우에 모델과 딕셔너리에 추가합니다.
                Point newPoint = null;
                if (newPointDictionary.TryGetValue(weightedPointSet, out newPoint) == false)
                {
                    newPoint = subdividedModel.AddPoint( weightedPointSet.GetInterpolatedPosition() );
                    newPointDictionary.Add(weightedPointSet, newPoint);
                }
            });

            // 새로운 모델에 폴리곤을 추가합니다.
            foreach (var polygon in sourceModel.Polygons)
            {
                polygon.ForEachSubTriangle((wP1, wP2, wP3) => {
                    Point newPoint1 = newPointDictionary[wP1];
                    Point newPoint2 = newPointDictionary[wP2];
                    Point newPoint3 = newPointDictionary[wP3];
                    Line line1 = subdividedModel.AddUniqueLine(newPoint1, newPoint2);
                    Line line2 = subdividedModel.AddUniqueLine(newPoint2, newPoint3);
                    Line line3 = subdividedModel.AddUniqueLine(newPoint3, newPoint1);
                    subdividedModel.AddPolygon(new Line[] { line1, line2, line3 });
                });
            }

            return subdividedModel;
        }
    }
}
