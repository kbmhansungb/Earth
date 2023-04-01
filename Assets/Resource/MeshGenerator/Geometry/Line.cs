using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MeshGenerator.Geometry
{
    public class Line
    {
        private bool m_isReversed;
        private Line m_reversedLine;

        private Point m_begin;
        private Point m_end;

        internal Polygon m_left;
        internal Polygon m_right;

        public bool IsReversed { get => m_isReversed; }
        public Line ReversedLine { get => m_reversedLine; }
        public Point Begin { get => m_begin; }
        public Point End { get => m_end; }
        public Polygon Left { get => m_left; }
        public Polygon Right { get => m_right; }

        private Line() { }

        public Line(Point begin, Point end)
        {
            m_isReversed = false;
            m_begin = begin;
            m_end = end;
            m_reversedLine = new Line();

            m_reversedLine.m_isReversed = true;
            m_reversedLine.m_begin = end;
            m_reversedLine.m_end = begin;
            m_reversedLine.m_reversedLine = this;

            // Point에 연결된 라인을 추가합니다.
            m_begin.AddLine(this);
            m_end.AddLine(this);
        }

        public void SetPolygon(Polygon polygon, bool isRight = true)
        {
            if (isRight)
            {
                m_right = polygon;
                m_reversedLine.m_left = polygon;
            }
            else
            {
                m_left = polygon;
                m_reversedLine.m_right = polygon;
            }
        }
    }
} 