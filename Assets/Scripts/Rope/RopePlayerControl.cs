using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePlayerControl : MonoBehaviour
{
    [SerializeField] HookParent hookParent;

    public void PlayerTap()
    {
        if (!hookParent.HookIsFree) return;

        hookParent.UnParentRopeObject();
    }
}
