using Mono.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModelGenerator.Geometry
{
    public partial class Model : Shape
    {
        /// <summary>
        /// Begin, End 두 점을 이은 선을 찾고, 없으면 새로 생성합니다.
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Line AddUniqueLine(Point begin, Point end)
        {
            Line line = GetLine(begin, end);
            if (line == null)
            {
                line = AddLine(begin, end);
            }
            return line;
        }

        /// <summary>
        /// Begine, End를 잊는 선을 추가합니다.
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Line AddLine(Point begin, Point end)
        {
            Line newLine = new Line(begin, end);
            m_lines.Add(newLine);
            return newLine;
        }

        /// <summary>
        /// 반전을 포함하여 A, B를 잊는 선을 찾습니다.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public Line GetLine(Point A, Point B)
        {
            foreach (var line in m_lines)
            {
                if (line.Begin == A && line.End == B)
                {
                    return line;
                }
                else if (line.Begin == B && line.End == A)
                {
                    return line.ReversedLine;
                }
            }

            return null;
        }
    }
}
