using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace MeshGenerator.Geometry
{
    /// <summary>
    /// A -> B로 가는 라인과 B -> A로 가는 라인은 반전된 라인입니다.
    /// </summary>
    public class Line
    {
        private bool m_isDefault;
        private Line m_reversedLine;

        private class Data
        {
            public Vertex Begin;
            public Vertex End;

            public Polygon Left;
            public Polygon Right;
        }
        private Data m_data = new Data();

        public Line ReversedLine { get => m_reversedLine; }
        public Vertex Begin { get => m_isDefault ? m_data.Begin : m_data.End; }
        public Vertex End { get => m_isDefault ? m_data.End : m_data.Begin; }
        public Polygon Left { 
            get => m_isDefault ? m_data.Left : m_data.Right; 
            set { 
                if (m_isDefault)
                    m_data.Left = value;
                else
                    m_data.Right = value;
            }
        }
        public Polygon Right { 
            get => m_isDefault ? m_data.Right : m_data.Left;
            set
            {
                if (m_isDefault)
                    m_data.Right = value;
                else
                    m_data.Left = value;
            }
        }

        private Line() { }

        public Line(Vertex begin, Vertex end)
        {
            m_isDefault = false;

            m_data.Begin = begin;
            m_data.End = end;

            m_reversedLine = new Line();
            m_reversedLine.m_isDefault = true;
            m_reversedLine.m_reversedLine = this;
            m_reversedLine.m_data = m_data;

            m_data.Begin.m_lines.Add(this);
            m_data.End.m_lines.Add(this);

            // 버텍스에 line을 추가합니다.
            begin.m_lines.Add(this);
            end.m_lines.Add(this);
        }
    }
} 