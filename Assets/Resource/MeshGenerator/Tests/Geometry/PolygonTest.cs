using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace ModelGenerator.Geometry
{
    public class PolygonTest
    {
        /// <summary>
        /// 면을 생성하고, 점과 선을 검사합니다.
        /// </summary>
        [Test]
        public void IsIntancedPolygonEqual()
        {
            (Polygon polygon, Line[] lines, Point[] vertices) = CreateTriangle();

            Assert.IsTrue(polygon.Points[0] == vertices[0], "Point 0 is not equal.");
            Assert.IsTrue(polygon.Points[1] == vertices[1], "Point 1 is not equal.");
            Assert.IsTrue(polygon.Points[2] == vertices[2], "Point 2 is not equal.");

            Assert.IsTrue(polygon.Lines[0] == lines[0], "Line 0 is not equal.");
            Assert.IsTrue(polygon.Lines[1] == lines[1], "Line 1 is not equal.");
            Assert.IsTrue(polygon.Lines[2] == lines[2], "Line 2 is not equal.");
        }

        /// <summary>
        /// 점과 선에 면이 추가되었는지 검사합니다.
        /// </summary>
        [Test]
        public void IsVerticiesAndLinesHavePolygon()
        {
            (Polygon polygon, Line[] lines, Point[] vertices) = CreateTriangle();

            Assert.IsTrue(vertices[0].Polygons.Count == 1 && vertices[0].Polygons[0] == polygon, "Polygons are not added correctly to vertex 0.");
            Assert.IsTrue(vertices[1].Polygons.Count == 1 && vertices[1].Polygons[0] == polygon, "Polygons are not added correctly to vertex 1.");
            Assert.IsTrue(vertices[2].Polygons.Count == 1 && vertices[2].Polygons[0] == polygon, "Polygons are not added correctly to vertex 2.");

            Assert.IsTrue(lines[0].Right == polygon, "Polygons are not added correctly to line 0.");
            Assert.IsTrue(lines[1].Right == polygon, "Polygons are not added correctly to line 1.");
            Assert.IsTrue(lines[2].Right == polygon, "Polygons are not added correctly to line 2.");
        }

        private (Polygon polygon, Line[] lines, Point[] vertices) CreateTriangle()
        {
            Point vertex1 = new Point(Vector3.zero);
            Point vertex2 = new Point(Vector3.right);
            Point vertex3 = new Point(Vector3.up);

            Line line1 = new Line(vertex1, vertex2);
            Line line2 = new Line(vertex2, vertex3);
            Line line3 = new Line(vertex3, vertex1);

            Point[] vertices = new Point[] { vertex1, vertex2, vertex3 };
            Line[] lines = new Line[] { line1, line2, line3 };
            Polygon polygon = new Polygon(new Line[]{ line1, line2, line3 });

            return (polygon, lines, vertices);
        }
    }
}
