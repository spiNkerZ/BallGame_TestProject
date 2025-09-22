using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : RopeObject
{
    public BallPreset_RopeObjectSO BallPreset => GetPreset() as BallPreset_RopeObjectSO;

    [SerializeField] BallFlyController ballFlyController;
    [SerializeField] BallEffects ballEffects;
    [SerializeField] BallPointsAdder ballPointsAdder;

    public override void UnParentThisObject()
    {
        ballFlyController.StartFly();
    }

    public override void DestroyThisObject()
    {
        ballEffects.PlayDestroyEffect();
        ballPointsAdder.AddPoints(BallPreset.GetCountPoints());
    }

}
