using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVisualView : MonoBehaviour
{
    [SerializeField] Ball ball;

    [SerializeField] SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer.color = ball.BallPreset.GetObjectColor();
    }

}
