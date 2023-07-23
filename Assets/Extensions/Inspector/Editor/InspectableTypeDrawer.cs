using UnityEngine;
using UnityEditor;
using System.Linq;

using Type = System.Type;


[CustomPropertyDrawer(typeof(InspectableType<>), true)]
public class InspectableTypeDrawer : PropertyDrawer
{
    System.Type[] m_derivedTypes;
    GUIContent[] m_optionLabels;
    int m_selectedIndex;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var storedProperty = property.FindPropertyRelative("m_qualifiedName");
        string qualifiedName = storedProperty.stringValue;

        if (m_optionLabels == null)
        {
            Initialize(property, storedProperty);
        }
        else if (m_selectedIndex == m_derivedTypes.Length)
        {
            if (qualifiedName != "null") UpdateIndex(storedProperty);
        }
        else
        {
            if (qualifiedName != m_derivedTypes[m_selectedIndex].AssemblyQualifiedName) UpdateIndex(storedProperty);
        }

        var propLabel = EditorGUI.BeginProperty(position, label, property);
        EditorGUI.BeginChangeCheck();

        m_selectedIndex = EditorGUI.Popup(position, propLabel, m_selectedIndex, m_optionLabels);

        if (EditorGUI.EndChangeCheck())
        {
            storedProperty.stringValue = m_selectedIndex < m_derivedTypes.Length ? m_derivedTypes[m_selectedIndex].AssemblyQualifiedName : "null";
        }
        EditorGUI.EndProperty();
    }

    static Type[] FindAllDerivedTypes(Type baseType)
    {
        return baseType.Assembly
            .GetTypes()
            .Where(t =>
                //t != baseType &&
                t.IsAbstract == false &&
                baseType.IsAssignableFrom(t)
                ).ToArray<Type>();
    }

    void Initialize(SerializedProperty property, SerializedProperty stored)
    {

        var baseTypeProperty = property.FindPropertyRelative("baseTypeName");
        var baseType = Type.GetType(baseTypeProperty.stringValue);

        m_derivedTypes = FindAllDerivedTypes(baseType);

        if (m_derivedTypes.Length == 0)
        {
            m_optionLabels = new[] { new GUIContent($"No types derived from {baseType.Name} found.") };
            return;
        }

        m_optionLabels = new GUIContent[m_derivedTypes.Length + 1];
        for (int i = 0; i < m_derivedTypes.Length; i++)
        {
            //m_optionLabels[i] = new GUIContent(m_derivedTypes[i].Name);
            m_optionLabels[i] = new GUIContent(m_derivedTypes[i].FullName);
        }
        m_optionLabels[m_derivedTypes.Length] = new GUIContent("null");

        UpdateIndex(stored);
    }

    void UpdateIndex(SerializedProperty stored)
    {
        string qualifiedName = stored.stringValue;

        for (int i = 0; i < m_derivedTypes.Length; i++)
        {
            if (m_derivedTypes[i].AssemblyQualifiedName == qualifiedName)
            {
                m_selectedIndex = i;
                return;
            }
        }
        m_selectedIndex = m_derivedTypes.Length;
        stored.stringValue = "null";
    }
}