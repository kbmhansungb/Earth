using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace MeshGenerator.Geometry
{
    public class LineTest
    {
        /// <summary>
        /// 라인의 Begin이 반전된 라인의 End와 같은지 검사하고
        /// 라인의 End가 반전된 라인의 Begin과 같은지 검사합니다.
        /// </summary>
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
