using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;


namespace ModelGenerator.Geometry
{
    [Serializable]
    public partial class Shape
    {
        private List<string> m_tags = new List<string>();

        public ReadOnlyCollection<string> Tags { get => m_tags.AsReadOnly(); }

        public void AddTag(string tag)
        {
            if (m_tags.Contains(tag)) 
                return;

            m_tags.Add(tag);
        }

        public void RemoveTag(string tag)
        {
            m_tags.Remove(tag);
        }
    }
}
