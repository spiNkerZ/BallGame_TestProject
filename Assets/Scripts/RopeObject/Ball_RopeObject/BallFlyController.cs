using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFlyController : MonoBehaviour
{
    [SerializeField] private Ball ball;

    public void StartFly()
    {
        CellData cellPoint = GameInstance.Instance.gameLinks.ballField.GetFreeCellForFly(ball);

        if (!cellPoint.CellIsFree) return;

        transform.DOMoveX(cellPoint.position.x, 0.4f).SetEase(Ease.Linear);

        transform.DOMoveY(cellPoint.position.y, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.SetParent(GameInstance.Instance.gameLinks.ballField.transform);

            GameInstance.Instance.gameLinks.ballField.OccupyCurrectCell(cellPoint, ball);
            GameInstance.Instance.gameLinks.ropeController.Reload();
        });
    }
}
