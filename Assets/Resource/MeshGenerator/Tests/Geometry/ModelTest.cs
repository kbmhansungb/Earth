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
        public void TestCreatePoint()
        {
            Model model = new Model();

            Point vertex = model.AddPoint(Vector3.zero);

            Assert.IsTrue(model.Points[0] == vertex && model.Points.Count == 1, "Point is not added correctly to model.");
        }

        /// <summary>
        /// 모델에서 라인이 생성되는지 검사합니다.
        /// </summary>
        [Test]
        public void TestCreateLine()
        {
            Model model = new Model();

            Point vertex1 = model.AddPoint(Vector3.zero);
            Point vertex2 = model.AddPoint(Vector3.up);

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

            Point vertex1 = model.AddPoint(Vector3.zero);
            Point vertex2 = model.AddPoint(Vector3.up);
            Point vertex3 = model.AddPoint(Vector3.right);

            Line line1 = model.AddLine(vertex1, vertex2);
            Line line2 = model.AddLine(vertex2, vertex3);
            Line line3 = model.AddLine(vertex3, vertex1);

            Polygon polygon = model.AddPolygon(new Line[] { line1, line2, line3 });

            Assert.IsTrue(model.Polygons[0] == polygon && model.Polygons.Count == 1, "Polygon is not added correctly to model.");
        }
    }
}
