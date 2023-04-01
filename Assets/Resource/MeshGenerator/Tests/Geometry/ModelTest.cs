using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;


namespace MeshGenerator.Geometry
{
    public class ModelTest
    {
        /// <summary>
        /// 모델에서 버택스를 생성되는지 검사합니다.
        /// </summary>
        [Test]
        public void TestCreateVertex()
        {
            Model model = new Model();

            Vertex vertex = model.AddVertex(Vector3.zero);

            Assert.IsTrue(model.Vertices[0] == vertex && model.Vertices.Count == 1, "Vertex is not added correctly to model.");
        }

        /// <summary>
        /// 모델에서 라인이 생성되는지 검사합니다.
        /// </summary>
        [Test]
        public void TestCreateLine()
        {
            Model model = new Model();

            Vertex vertex1 = model.AddVertex(Vector3.zero);
            Vertex vertex2 = model.AddVertex(Vector3.up);

            Line line = model.AddLine(vertex1, vertex2);

            Assert.IsTrue(model.Lines[0] == line && model.Lines.Count == 1, "Line is not added correctly to model.");
        }

        /// <summary>
        /// 모델에서 폴리곤이 생성되는지 검사합니다.
        /// </summary>
        [Test]
        public void TestCreatePolygon()
        {
            Model model = new Model();

            Vertex vertex1 = model.AddVertex(Vector3.zero);
            Vertex vertex2 = model.AddVertex(Vector3.up);
            Vertex vertex3 = model.AddVertex(Vector3.right);

            Line line1 = model.AddLine(vertex1, vertex2);
            Line line2 = model.AddLine(vertex2, vertex3);
            Line line3 = model.AddLine(vertex3, vertex1);

            Polygon polygon = model.AddPolygon(new Line[] { line1, line2, line3 });

            Assert.IsTrue(model.Polygons[0] == polygon && model.Polygons.Count == 1, "Polygon is not added correctly to model.");
        }
    }
}
