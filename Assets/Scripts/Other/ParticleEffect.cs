using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    public void Create(Vector3 _pos, float _timeDestroy = 5)
    {
        Destroy(Instantiate(gameObject,_pos, Quaternion.identity),_timeDestroy);
    }
}
