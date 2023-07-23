using System;
using UnityEngine;

[Serializable]
public class InspectableType<T> : ISerializationCallbackReceiver
{
    [SerializeField] string m_qualifiedName;
    private Type m_storedType;

    public Type StoredType => m_storedType;

#if UNITY_EDITOR
    // TODO : SerializedProperty에서 기본 유형을 찾을 수 없어서 편집기에서만 저장되는 추가 문자열을 통해 이를 구해오고 있습니다.
    [SerializeField] string baseTypeName;
#endif

    public InspectableType(Type typeToStore)
    {
        m_storedType = typeToStore;
    }

    public override string ToString()
    {
        if (m_storedType == null) return string.Empty;
        return m_storedType.Name;
    }

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

    public static implicit operator System.Type(InspectableType<T> t) => t.m_storedType;
    public static implicit operator InspectableType<T>(System.Type t) => new InspectableType<T>(t);
}