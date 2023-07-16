using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ShapeGrammer
{
    /// <summary>
    /// Shape Grammar에서 형태의 구성 요소를 나타내는 기호 또는 식별자입니다. 예를 들어, 건축 설계에서 "벽"이나 "문"과 같은 심볼을 정의할 수 있습니다. 심볼은 메쉬, 객체, 구조물 또는 다른 형태를 나타낼 수 있습니다.
    /// 각 심볼은 속성을 가질 수 있습니다. 속성은 형태의 특성이나 속성을 설명하는 정보입니다. 예를 들어, 크기, 위치, 회전, 색상 등의 속성을 가질 수 있습니다.
    /// </summary>
    public partial interface ISymbolable
    {
        /// <summary>
        /// 심볼 위치를 반환합니다.
        /// </summary>
        /// <returns>심볼의 위치입니다.</returns>
        public Vector3 GetSymbolPosition();

        /// <summary>
        /// 심볼 회전을 반환합니다.
        /// </summary>
        /// <return>심볼의 회전입니다.</return>
        public Quaternion GetSymbolRotation();

        /// <summary>
        /// 심볼 크기를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetSymbolSize();
    }
}