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

        private Vertex m_begin;
        private Vertex m_end;

        internal Polygon m_left;
        internal Polygon m_right;

        public bool IsReversed { get => m_isReversed; }
        public Line ReversedLine { get => m_reversedLine; }
        public Vertex Begin { get => m_begin; }
        public Vertex End { get => m_end; }
        public Polygon Left { get => m_left; }
        public Polygon Right { get => m_right; }

        private Line() { }

        public Line(Vertex begin, Vertex end)
        {
            m_isReversed = false;
            m_begin = begin;
            m_end = end;
            m_reversedLine = new Line();

            m_reversedLine.m_isReversed = true;
            m_reversedLine.m_begin = end;
            m_reversedLine.m_end = begin;
            m_reversedLine.m_reversedLine = this;

            // Vertex에 연결된 라인을 추가합니다.
            m_begin.m_lines.Add(this);
            m_end.m_lines.Add(this);
        }
    }
} 