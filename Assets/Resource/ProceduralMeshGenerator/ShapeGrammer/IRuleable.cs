using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShapeGrammer
{
    /// <summary>
    /// 규칙은 형태의 생성 또는 변형을 정의하는 문장입니다. 각 규칙은 입력 심볼에 대한 출력 심볼의 규칙을 나타냅니다. 
    /// 규칙은 하나 이상의 심볼을 생성하거나 변형시킬 수 있습니다. 예를 들어, "벽" 심볼을 "창문"과 "문"으로 분할하는 규칙을 정의할 수 있습니다.
    /// </summary>
    public partial interface IRuleable
    {
    }
}