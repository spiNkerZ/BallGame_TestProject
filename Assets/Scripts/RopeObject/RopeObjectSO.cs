using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RopeObject")]
public class RopeObjectSO : ScriptableObject
{
    public AmplitudeDataS amplitudeData;

    [SerializeField] private RopeObject ropeObjectPrefab;

    public RopeObject Create()
    {
        RopeObject ropeObjectSpawned = Instantiate(ropeObjectPrefab);
        ropeObjectSpawned.Init(this);

        return ropeObjectSpawned;
    }

}
