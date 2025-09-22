using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookParent : MonoBehaviour
{
    public bool HookIsFree => transform.childCount > 0;

    private RopeObject ropeObjectInHook;

    public void InsertRopeObject(RopeObject _ropeObject)
    {
        ropeObjectInHook = _ropeObject;

        ropeObjectInHook.transform.SetParent(transform);
    }

    public void UnParentRopeObject()
    {
        if (GameInstance.Instance.gameLinks.ballField.CheckCellForFly(ropeObjectInHook)) return;

        ropeObjectInHook.transform.SetParent(null);

        ropeObjectInHook.UnParentThisObject();

        ropeObjectInHook = null;
    }
}
