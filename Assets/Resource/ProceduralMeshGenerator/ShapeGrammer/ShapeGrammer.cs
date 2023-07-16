using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ShapeGrammer
{
    public partial class ShapeGrammer
    {
        protected List<ISymbolable> m_symbolables = new List<ISymbolable>();
        protected Dictionary<Type, List<IRuleable>> m_SymbolRules = new Dictionary<Type, List<IRuleable>>();

        public ReadOnlyCollection<ISymbolable> Symbolables { get => m_symbolables.AsReadOnly(); }

        public void AddSymbolable(ISymbolable symbolable)
        {
            m_symbolables.Add(symbolable);
        }

        public void RemoveSymbolable(ISymbolable symbolable)
        {
            m_symbolables.Remove(symbolable);
        }

        public void AddRule<SymbolType>(IRuleable rule)
            where SymbolType : ISymbolable
        {
            Type symbolType = typeof(SymbolType);
            if (m_SymbolRules.ContainsKey(symbolType))
            {
                m_SymbolRules[symbolType].Add(rule);
            }
            else
            {
                m_SymbolRules.Add(symbolType, new List<IRuleable>() { rule });
            }
        }

        public void RemoveRule<SymbolType>(IRuleable rule)
            where SymbolType : ISymbolable
        {
            Type symbolType = typeof(SymbolType);
            if (m_SymbolRules.ContainsKey(symbolType))
            {
                m_SymbolRules[symbolType].Remove(rule);
            }
        }
    }
}