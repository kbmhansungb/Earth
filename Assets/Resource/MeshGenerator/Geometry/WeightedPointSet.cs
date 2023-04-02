using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModelGenerator.Geometry
{
    public class WeightedPointSet
    {
        private SortedSet<(Point point, float weight)> weightedPointSet = new SortedSet<(Point point, float weight)>();

        public WeightedPointSet(params (Point point, float weight)[] weightedPoints)
        {
            AddWeightedPoint(weightedPoints);
        }

        public void AddWeightedPoint(params (Point point, float weight)[] weightedPoints)
        {
            foreach (var weightedPoint in weightedPoints)
            {
                if (weightedPoint.weight != 0)
                {
                    weightedPointSet.Add(weightedPoint);
                }
            }
        }

        public Vector3 GetInterpolatedPosition()
        {
            Vector3 interpolatedPosition = Vector3.zero;

            foreach (var weightedPoint in weightedPointSet)
            {
                interpolatedPosition += weightedPoint.point.Position * weightedPoint.weight;
            }

            return interpolatedPosition;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                foreach (var weightedPoint in weightedPointSet)
                {
                    // Insert()는 해시값과 Equals()를 이용해 모두 true일 경우 중복 키 삽입으로 판단합니다.
                    hash = hash * 23 + weightedPoint.point.Position.GetHashCode();
                    hash = hash * 23 + weightedPoint.weight.GetHashCode();
                }
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            { 
                return false;
            }

            WeightedPointSet other = (WeightedPointSet)obj;

            if (this.weightedPointSet.Count != other.weightedPointSet.Count)
            {
                return false;
            }

            var thisEnumerator = this.weightedPointSet.GetEnumerator();
            var otherEnumerator = other.weightedPointSet.GetEnumerator();
            while (thisEnumerator.MoveNext() && otherEnumerator.MoveNext())
            {
                if (thisEnumerator.Current.point != otherEnumerator.Current.point)
                {
                    return false;
                }

                if (thisEnumerator.Current.weight != otherEnumerator.Current.weight)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
