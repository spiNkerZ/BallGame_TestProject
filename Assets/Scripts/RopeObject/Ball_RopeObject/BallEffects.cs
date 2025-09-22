using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffects : MonoBehaviour
{
    [SerializeField] ParticleEffect destroyEffect;

    public void PlayDestroyEffect()
    {
        destroyEffect.Create(transform.position);

        transform.DOScale(Vector2.zero, 0.6f).SetEase(Ease.OutBounce).OnComplete(() => Destroy(gameObject));
    }
}
