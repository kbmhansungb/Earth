using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShapeGrammer
{
    [Serializable]
    class ExpandRule : Rule
    {
        [Header("ExpandRule")]
        [SerializeField] private Vector3 m_expandAxis;
        [SerializeField] private bool m_checkCollision;
    }
}
