using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton_ClickHandler : MonoBehaviour
{
    private bool isClicked = false;

    private void OnMouseDown()
    {
        GameStatus gameStatus = GameInstance.Instance.gameController.GameStatus;

        if (gameStatus != GameStatus.WaitStartGame) return;

        if (isClicked) return;

        GameInstance.Instance.gameController.OnClickStartGameFromPreviewGame();

        isClicked = true;
    }
}
