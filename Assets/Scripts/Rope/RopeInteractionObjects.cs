using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeInteractionObjects : MonoBehaviour
{
    [SerializeField] Transform spawnPoint, ropePivot;

    [SerializeField] HookParent hookParent;

    [SerializeField] float offsetEndPointY, ampltitudeEaseFactor, ampltitudeEaseFactorReload;

    private RopeObject ropeObjectSpawned;
    private AmplitudeDataS ampltideData;

    private Vector2 endPoint;

    public RopeObject RopeObjectSpawned => ropeObjectSpawned;

    private void Awake()
    {
        endPoint = (Vector2)hookParent.transform.position + Vector2.down * offsetEndPointY;
    }

    public void SpawnRopeObject(RopeObject _ropeObject, AmplitudeDataS _amplitudeData, bool _isReload)
    {
        ropeObjectSpawned = _ropeObject;
        ampltideData = _amplitudeData;

        ropeObjectSpawned.transform.position = spawnPoint.position;

        StartFlyAnimation(_isReload);
    }

    private void StartFlyAnimation(bool _isReload = false)
    {
        ropeObjectSpawned.transform.DOMoveY(endPoint.y, (ampltideData.amplitudeSpeed / 2) * (_isReload ? ampltitudeEaseFactorReload : ampltitudeEaseFactor)).SetEase(Ease.Linear).OnComplete(() =>
        {
            hookParent.InsertRopeObject(ropeObjectSpawned);
        });
    }
}
