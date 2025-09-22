using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAmplitudeAnimation : MonoBehaviour
{
    [SerializeField] Transform ropePivot;

    [SerializeField] float maxAngleAmplitude;

    [SerializeField] Ease easeAmplitude;

    [SerializeField] RopeInteractionObjects ropeInterectionObjects;

    private AmplitudeDataS amplitudeData;

    private DG.Tweening.Core.TweenerCore<Quaternion, Quaternion, DG.Tweening.Plugins.Options.NoOptions> animationTween;

    public void StartAnimation(AmplitudeDataS _amplitudeData)
    {
        ropePivot.transform.rotation = Quaternion.Euler(0, 0, 90);

        amplitudeData = _amplitudeData;

        StartCoroutine(AnimationCorutine());
    }

    public void SubsrabeToAmplitudeTick(TweenCallback _action)
    {
        animationTween.onStepComplete += _action;
    }

    public void UnSubsrabeToAmplitudeTick(TweenCallback _action)
    {
        animationTween.onStepComplete -= _action;
    }

    public IEnumerator StopAmplitudeAnimation()
    {
        animationTween.Kill(true);

        yield return ropePivot.DORotateQuaternion(Quaternion.Euler(0, 0, 0), amplitudeData.amplitudeSpeed).SetEase(Ease.InOutQuad).WaitForCompletion();  

        animationTween.startValue = Quaternion.Euler(Vector3.zero);

        yield return animationTween.WaitForElapsedLoops(1);
    }

    private IEnumerator AnimationCorutine()
    {
        animationTween = ropePivot.DORotateQuaternion(Quaternion.Euler(0, 0, -maxAngleAmplitude), amplitudeData.amplitudeSpeed).SetEase(easeAmplitude).SetLoops(-1, LoopType.Yoyo);

        yield return animationTween.WaitForElapsedLoops(1);

        animationTween.startValue = Quaternion.Euler(0, 0, maxAngleAmplitude);
    }
}

