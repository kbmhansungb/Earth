using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShapeGrammer
{
    [Serializable]
    class ReplaceRule : Rule
    {
        [Header("ReplaceRule")]
        [SerializeField] private GameObject prefab;
    }
}
