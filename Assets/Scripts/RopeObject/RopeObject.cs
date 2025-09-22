using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RopeObject : MonoBehaviour
{
    public RopeObjectSO preset;

    public void Init(RopeObjectSO _ropeObjectPreset)
    {
        preset = _ropeObjectPreset;
    }

    protected virtual RopeObjectSO GetPreset()
    {
        return preset;
    }

    public virtual void UnParentThisObject() { }

    public virtual void DestroyThisObject() { }
}
