using ModelGenerator.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static ShapeGrammer.IRuleable;

namespace ShapeGrammer
{
    [Serializable]
    abstract class Rule : IRuleable
    {
        public List<TagData> SearchTag;
        public List<TagData> ResultTag;


        [Serializable]
        public struct TagData
        {
            public InspectableType<Shape> Shape;
            public string Tag;
        }
    }
}
