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
        /// Line을 생성하고 Begin과 End가 일치하는지 검사합니다.
        /// </summary>
        [Test]
        public void IsInstancedLineBeginEndEqual()
        {
            (Line line, Vertex vertex1, Vertex vertex2) = CretaeLine();
            
            bool isBeginEqual = line.Begin == vertex1;
            bool isEndEqual = line.End == vertex2;

            Assert.IsTrue(isBeginEqual, "Line`s Begin is not equal.");
            Assert.IsTrue(isEndEqual, "Line`s Begin is not equal.");
        }

        /// <summary>
        /// 반전된 라인이 생성되었는지 검사하고,
        /// 그리고 반전된 라인의 반전이 다시 원래의 라인인지 검사합니다.
        /// </summary>
        [Test]
        public void IsReversedLineInstanccing()
        {
            (Line line, _, _) = CretaeLine();

            Assert.IsNotNull(line.ReversedLine, "Reversed line is not creat.");
            Assert.IsTrue(line == line.ReversedLine.ReversedLine, "Reversed line is not initialized.");
        }

        /// <summary>
        /// 라인의 Begin이 반전된 라인의 End와 같은지 검사하고
        /// 라인의 End가 반전된 라인의 Begin과 같은지 검사합니다.
        /// </summary>
        [Test]
        public void IsReversedLineVertexEqual()
        {
            (Line line, _, _) = CretaeLine();

            bool isReversedBegin = line.Begin == line.ReversedLine.End;
            bool isReversedEnd = line.End== line.ReversedLine.Begin;

            Assert.IsTrue(isReversedBegin, "Line`s Begin vertex is different.");
            Assert.IsTrue(isReversedEnd, "Line`s End vertex is different.");
        }

        private (Line line, Vertex vertex1, Vertex vertex2) CretaeLine()
        {
            Vertex vertex1 = new Vertex(Vector3.zero);
            Vertex vertex2 = new Vertex(Vector3.right);

            Line line = new Line(vertex1, vertex2);

            return (line, vertex1, vertex2);
        }
    }
}
