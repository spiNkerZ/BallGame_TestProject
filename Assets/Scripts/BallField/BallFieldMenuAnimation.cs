using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFieldMenuAnimation : MonoBehaviour
{
    [SerializeField] private Transform ballFieldAnimationEndPoint, ballFieldAnimationStartPoint;

    [SerializeField] private float ballFieldAnimationDuration;

    public void ResetAnimation()
    {
        gameObject.SetActive(false);

        transform.position = ballFieldAnimationStartPoint.position;
    }

    public IEnumerator StartAnimationViewBallField()
    {
        gameObject.SetActive(true);

        yield return transform.DOMove(ballFieldAnimationEndPoint.position, ballFieldAnimationDuration).SetEase(Ease.OutBounce).WaitForCompletion(); 
    }
    public IEnumerator StartAnimationGameOver()
    {
        yield return transform.DOMove(ballFieldAnimationStartPoint.position, ballFieldAnimationDuration + 0.5f).SetEase(Ease.Linear).WaitForCompletion();

        gameObject.SetActive(true);
    }

}
