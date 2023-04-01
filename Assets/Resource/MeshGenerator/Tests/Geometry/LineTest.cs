using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace MeshGenerator.Geometry
{
    public class LineTest
    {
        [Test]
        public void IsReversedLineVertexEqual()
        {
            Vertex vertex1 = new Vertex(Vector3.zero);
            Vertex vertex2 = new Vertex(Vector3.right);

            Line line = new Line(vertex1, vertex2);

            bool isReversedBegin = line.Begin == line.ReversedLine.End;
            bool isReversedEnd = line.End== line.ReversedLine.Begin;
            bool isReversed = isReversedBegin && isReversedEnd;

            Assert.IsTrue(isReversed, "Line`s vertex is different.");
        }
    }
}
