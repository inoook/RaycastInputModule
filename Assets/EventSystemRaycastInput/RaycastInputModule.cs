using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// http://edom18.hateblo.jp/entry/2017/05/24/204443
// http://blog.d-yama7.com/archives/190
// https://bitbucket.org/Unity-Technologies/ui/src/0bd08e22bc17/UnityEngine.UI/?at=5.2
// https://github.com/tenpn/unity3d-ui/blob/master/UnityEngine.UI/UI/Core/Selectable.cs

[RequireComponent(typeof(RaycastInput))]
public class RaycastInputModule : StandaloneInputModule
{
    [SerializeField] RaycastInput overrideBaseInput;

    protected override void Awake() {
        overrideBaseInput = this.gameObject.GetComponent<RaycastInput>();
        if (overrideBaseInput == null) {
            overrideBaseInput = this.gameObject.AddComponent<RaycastInput>();
        }
        m_InputOverride = overrideBaseInput;
    }

}
