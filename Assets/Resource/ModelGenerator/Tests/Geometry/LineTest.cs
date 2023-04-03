using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;


namespace ModelGenerator.Geometry
{
    public class LineTest
    {
        /// <summary>
        /// Line을 생성하고 Begin과 End가 일치하는지 검사합니다.
        /// </summary>
        [Test]
        public void IsInstancedLineBeginEndEqual()
        {
            (Line line, Point vertex1, Point vertex2) = CretaeLine();
            
            bool isBeginEqual = line.Begin == vertex1;
            bool isEndEqual = line.End == vertex2;

            Assert.IsTrue(isBeginEqual, "Line`s Begin is not equal.");
            Assert.IsTrue(isEndEqual, "Line`s Begin is not equal.");
        }

        /// <summary>
        /// 생성된 점에 선이 추가되었는지 검사합니다.
        /// 선은 점에 추가되어야 하지만, 반전된 선은 점에 추가되면 안됩니다.
        /// </summary>
        [Test]
        public void IsPointHaveLine()
        {
            (Line line, Point begin, Point end) = CretaeLine();

            Assert.IsTrue(begin.Lines[0] == line && begin.Lines.Count == 1, "Line are not added correctly to begin vertex.");
            Assert.IsTrue(end.Lines[0] == line && end.Lines.Count == 1, "Line are not added correctly to end vertex.");
        }

        /// <summary>
        /// 반전된 선이 생성되었는지 검사하고,
        /// 그리고 반전된 선의 반전이 다시 원래의 선인지 검사합니다.
        /// </summary>
        [Test]
        public void IsReversedLineInstanccing()
        {
            (Line line, _, _) = CretaeLine();

            Assert.IsTrue(line.IsReversed == false, "Default line is not reversed line.");

            Assert.IsTrue(line.ReversedLine.IsReversed == true, "Reversed line is not reversed.");
            Assert.IsNotNull(line.ReversedLine, "Reversed line is not creat.");
            Assert.IsTrue(line == line.ReversedLine.ReversedLine, "Reversed line is not initialized.");
        }

        /// <summary>
        /// 선의 시작이 반전된 선의 끝과 같은지 검사하고
        /// 선의 끝이 반전된 선의 시작과 같은지 검사합니다.
        /// </summary>
        [Test]
        public void IsReversedLinePointEqual()
        {
            (Line line, _, _) = CretaeLine();

            bool isReversedBegin = line.Begin == line.ReversedLine.End;
            bool isReversedEnd = line.End== line.ReversedLine.Begin;

            Assert.IsTrue(isReversedBegin, "Line`s Begin vertex is different.");
            Assert.IsTrue(isReversedEnd, "Line`s End vertex is different.");
        }

        private (Line line, Point vertex1, Point vertex2) CretaeLine()
        {
            Point vertex1 = new Point(Vector3.zero);
            Point vertex2 = new Point(Vector3.right);

            Line line = new Line(vertex1, vertex2);

            return (line, vertex1, vertex2);
        }
    }
}
