using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ShapeGrammer
{
    /// <summary>
    /// <see cref="ShapeGrammer"/>는 형상 문법에서 해석기의 역활을 합니다.
    /// 해석기는 문법을 해석하고, 규칙을 적용하여 형상을 생성하는 역할을 합니다. 해석기는 주어진 심볼을 해석하고, 해당 심볼에 대한 규칙을 찾아 규칙에 따라 메쉬 생성 또는 변형을 수행합니다.
    /// </summary>
    public partial class ShapeGrammer
    {
        protected List<ISymbolable> m_symbolables = new List<ISymbolable>();
        
        public ReadOnlyCollection<ISymbolable> Symbolables { get => m_symbolables.AsReadOnly(); }

        public void AddSymbolable(ISymbolable symbolable)
        {
            m_symbolables.Add(symbolable);
        }

        public void RemoveSymbolable(ISymbolable symbolable)
        {
            m_symbolables.Remove(symbolable);
        }
    }
}