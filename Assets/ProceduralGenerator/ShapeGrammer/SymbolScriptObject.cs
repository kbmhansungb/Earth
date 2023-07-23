using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;



namespace ShapeGrammer
{
    [CreateAssetMenu(fileName = "SymbolScriptObject", menuName = "ShapeGrammer/Symbol", order = 1)]
    public class SymbolScriptObject : ScriptableObject, ISymbolable
    {
        [SerializeReference, SubclassSelector] private List<Rule> m_rules = new List<Rule>();

        #region ISymbolable

        public ReadOnlyCollection<IRuleable> GetRules()
        {
            throw new System.NotImplementedException();
        }

        public Vector3 GetSymbolPosition()
        {
            throw new System.NotImplementedException();
        }

        public Quaternion GetSymbolRotation()
        {
            throw new System.NotImplementedException();
        }

        public Vector3 GetSymbolSize()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
