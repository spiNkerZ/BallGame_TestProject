using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIPanel : UIPanelBase
{
    [SerializeField] private TextMeshProUGUI gameOverTMP, reawrdTMP, rewardCountText;

    [SerializeField] CanvasGroup restartButtonGroup, inMenuButtonGroup;

    public override void Show()
    {
        gameOverTMP.DOFade(0, 1).Complete();
        reawrdTMP.DOFade(0, 1).Complete();
        restartButtonGroup.DOFade(0, 1).Complete();
        inMenuButtonGroup.DOFade(0, 1).Complete();
        rewardCountText.text = "";

        base.Show();

        StartCoroutine(AnimationGameOver());
    }

    private IEnumerator AnimationGameOver()
    {
        yield return gameOverTMP.DOFade(1, 1).SetEase(Ease.Linear).WaitForCompletion();

        yield return new WaitForSeconds(1);

        yield return reawrdTMP.DOFade(1, 1).SetEase(Ease.Linear).WaitForCompletion();

        int countReward = GameInstance.Instance.gameLinks.statsAssembler.CountPoints;
        int countRewardWhile = 0;

        rewardCountText.text = $"{countRewardWhile}";

        while (countRewardWhile != countReward)
        {
            rewardCountText.DOKill();

            rewardCountText.text = $"{countRewardWhile}";

            countRewardWhile++;

            rewardCountText.transform.DOPunchScale(Vector3.one * 1.05f, 0.2f, 1, 0).SetEase(Ease.Flash);

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);

        yield return restartButtonGroup.DOFade(1, 1).SetEase(Ease.Linear).WaitForCompletion();
        yield return inMenuButtonGroup.DOFade(1, 1).SetEase(Ease.Linear).WaitForCompletion();
    }

    public void OnClick_Restart()
    {
        GameInstance.Instance.gameController.RestartGame();
    }

    public void OnClick_InMenu()
    {
        GameInstance.Instance.gameController.InGameMenu();
    }
}
