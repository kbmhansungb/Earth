using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace MeshGenerator.Geometry
{
    public class VertexTest
    {
        [Test]
        public void IsInstancedVertexPositionEqual()
        {
            Vector3 vertexPosition = new Vector3(1.0f, 0.0f, 0.0f);
            Vertex vertex = new Vertex(vertexPosition);
            Assert.IsTrue(vertex.Position == vertexPosition, "Vertex position is different.");
        }
    }
}
