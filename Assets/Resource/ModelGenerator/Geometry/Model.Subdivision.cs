using Codice.Client.BaseCommands;
using ModelGenerator.Geometry;
using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace ModelGenerator.Geometry
{
    public partial class Model
    {
        public Model CreateSubdivision()
        {
            Dictionary<WeightedPointSet, Point> newPointDictionary = new Dictionary<WeightedPointSet, Point>();

            Model subdividedModel = new Model();

            int spliteCount = 1;

            ForEachPointBySpliteLine(spliteCount, weightedPointSet => {
                // 점이 없을 경우에 모델과 딕셔너리에 추가합니다.
                Point newPoint = null;
                if (newPointDictionary.TryGetValue(weightedPointSet, out newPoint) == false)
                {
                    newPoint = subdividedModel.AddPoint(weightedPointSet.GetInterpolatedPosition());
                    newPointDictionary.Add(weightedPointSet, newPoint);
                }
            });

            // 새로운 모델에 폴리곤을 추가합니다.
            foreach (var polygon in Polygons)
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

        public void ForEachPointBySpliteLine(int spliteCount, Action<WeightedPointSet> action)
        {
            foreach(var line in Lines)
            {
                for (int index = 0; index <= spliteCount + 1; index++)
                {
                    float weight = (float) index / (spliteCount + 1);
                    action.Invoke( new WeightedPointSet((line.Begin, 1 - weight), (line.End, weight)) );
                }
            }
        }
    }
}
