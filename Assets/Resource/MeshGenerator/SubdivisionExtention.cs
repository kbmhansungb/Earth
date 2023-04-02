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
    }

    public class SubdivisionGenerator : ModelGenerator
    {
        public Model CreateSubdivision(Model sourceModel)
        {
            Dictionary<WeightedPointSet, Point> newPointDictionary = new Dictionary<WeightedPointSet, Point>();

            Model subdividedModel = new Model();

            int spliteCount = 1;
            sourceModel.ForEachPointBySpliteLine(spliteCount, weightedPointSet => {
                Point newPoint = null;
                if (newPointDictionary.TryGetValue(weightedPointSet, out newPoint) == false)
                {
                    newPoint = subdividedModel.AddPoint( weightedPointSet.GetInterpolatedPosition() );
                    newPointDictionary.Add(weightedPointSet, newPoint);
                }
            });

            return subdividedModel;
        }
    }
}
