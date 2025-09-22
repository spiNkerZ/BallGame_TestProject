using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RopeMenuAnimation : MonoBehaviour
{
    [SerializeField] private RopeAmplitudeAnimation ropeAmplitudeAnimation;
    [SerializeField] private RopeInteractionObjects ropeInteractionObjects;

    [SerializeField] private int countTicksToUnHookNeed;

    [SerializeField] private Transform startButtonEndAnimationPoint;
    [SerializeField] private float speedMoveButtonToEndAnimationPoint;
    [SerializeField] private Ease easeMoveButtonToEndAnimationPoint;

    [SerializeField] private Transform gameOverAnimationMoveToPoint;
    [SerializeField] private Ease easeGameOverAnimationMoveToPoint;

    private int countAmplitudeTickNow;


    public void StartMenuAnimation()
    {
        RopeObjectSO ropeObjectUse = GameInstance.Instance.levelData.menuRopeObject;

        ropeAmplitudeAnimation.StartAnimation(ropeObjectUse.amplitudeData);
        ropeInteractionObjects.SpawnRopeObject(ropeObjectUse.Create(), ropeObjectUse.amplitudeData, false);

        ropeAmplitudeAnimation.SubsrabeToAmplitudeTick(AmplitudeTick);
    }

    public void StartMenuWithoutAnimation()
    {
       RopeObjectSO ropeObjectUse = GameInstance.Instance.levelData.menuRopeObject;

       ropeAmplitudeAnimation.StartAnimation(ropeObjectUse.amplitudeData);

       //// StartCoroutine(AnimationStepTwo());

       // //OnCompleteMenuAnimation = _onComplete;

       // ropeAmplitudeAnimation.SubsrabeToAmplitudeTick(AmplitudeTick);
    }

    private void AmplitudeTick()
    {
        countAmplitudeTickNow++;

        if(countAmplitudeTickNow >= countTicksToUnHookNeed)
        {
            StartCoroutine(AnimationStepTwo());

            ropeAmplitudeAnimation.UnSubsrabeToAmplitudeTick(AmplitudeTick);
        }
    }

    private IEnumerator AnimationStepTwo()
    {
        RopeObject startButtonRopeObject = ropeInteractionObjects.RopeObjectSpawned;

        yield return new WaitForSeconds(startButtonRopeObject.preset.amplitudeData.amplitudeSpeed / 2);

        startButtonRopeObject.transform.SetParent(null);

        startButtonRopeObject.transform.DOScale(Vector2.one * 1.1f, 0.3f).SetDelay(speedMoveButtonToEndAnimationPoint - speedMoveButtonToEndAnimationPoint * 0.75f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);

        yield return startButtonRopeObject.transform.DOMove(startButtonEndAnimationPoint.position, speedMoveButtonToEndAnimationPoint)
        .SetEase(easeMoveButtonToEndAnimationPoint).WaitForCompletion();

        GameInstance.Instance.gameController.PrivewAnimationEnd();
    }

    public IEnumerator PlayAnimationEnd()
    {
        RopeObject startButtonRopeObject = ropeInteractionObjects.RopeObjectSpawned;

        startButtonRopeObject.transform.DOPunchScale(Vector3.one * 1.1f, 0.8f, 1, 3).SetEase(Ease.Flash);

        yield return startButtonRopeObject.transform.DOJump(startButtonRopeObject.transform.position + Vector3.up, 3,1,1).SetEase(Ease.Flash).WaitForCompletion();
        yield return startButtonRopeObject.transform.DOMove(startButtonRopeObject.transform.position + Vector3.down * 4,0.4f).SetEase(Ease.Flash).WaitForCompletion();

        startButtonRopeObject.DOKill();
    }

    public IEnumerator PlayAnimationGameOver()
    {
       yield return transform.DOMove(gameOverAnimationMoveToPoint.position, 2).SetEase(easeGameOverAnimationMoveToPoint).WaitForCompletion();
    }
}
