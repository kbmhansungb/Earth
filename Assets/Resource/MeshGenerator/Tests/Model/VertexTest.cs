using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace MeshGenerator
{
    /// <summary>
    /// 버택스를 생성하고 포지션이 제대로 할당되었는지 검사합니다.
    /// </summary>
    public class VertexTest
    {
        [Test]
        public void IsInstancedVertexPositionEqual()
        {
            Vector3 vertexPosition = new Vector3(1.0f, 0.0f, 0.0f);

            Vertex m_vertex = new Vertex(vertexPosition);

            Assert.IsTrue(m_vertex.Position == vertexPosition, "Vertex position is different.");
        }
    }
}
