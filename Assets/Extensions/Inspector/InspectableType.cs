using System;
using UnityEngine;

[Serializable]
public struct InspectableType<T> : ISerializationCallbackReceiver
{
    [SerializeField] private string m_qualifiedName;
    private Type m_storedType;

#if UNITY_EDITOR
    [SerializeField] private string baseTypeName;
#endif

    public Type StoredType { get => m_storedType; }


    #region ISerializationCallbackReceiver

    public void OnBeforeSerialize()
    {
        m_qualifiedName = m_storedType?.AssemblyQualifiedName;

#if UNITY_EDITOR
        baseTypeName = typeof(T).AssemblyQualifiedName;
#endif
    }

    public void OnAfterDeserialize()
    {
        if (string.IsNullOrEmpty(m_qualifiedName) || m_qualifiedName == "null")
        {
            m_storedType = null;
            return;
        }
        m_storedType = Type.GetType(m_qualifiedName);
    }

    #endregion
}