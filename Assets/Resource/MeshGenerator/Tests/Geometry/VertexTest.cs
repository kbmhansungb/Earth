using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace MeshGenerator.Geometry
{
    public class PointTest
    {
        /// <summary>
        /// 점을 생성하고 점의 좌표가 일치하는지 검사합니다.
        /// </summary>
        [Test]
        public void IsInstancedPointPositionEqual()
        {
            Vector3 vertexPosition = new Vector3(1.0f, 0.0f, 0.0f);
            Point vertex = new Point(vertexPosition);
            Assert.IsTrue(vertex.Position == vertexPosition, "Point position is different.");
        }
    }
}
