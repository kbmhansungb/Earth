using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace MeshGenerator.Geometry
{
    public class VertexTest
    {
        /// <summary>
        /// 버택스를 생성하고 포지션이 일치하는지 검사합니다.
        /// </summary>
        [Test]
        public void IsInstancedVertexPositionEqual()
        {
            Vector3 vertexPosition = new Vector3(1.0f, 0.0f, 0.0f);
            Vertex vertex = new Vertex(vertexPosition);
            Assert.IsTrue(vertex.Position == vertexPosition, "Vertex position is different.");
        }
    }
}
