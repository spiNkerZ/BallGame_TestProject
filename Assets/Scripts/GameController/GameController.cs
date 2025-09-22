using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameStatus GameStatus
    {
        get { return gameStatus; }
    }

    private GameStatus gameStatus;

    private GameInstance Instance => GameInstance.Instance;

    private void Start()
    {
       if(GameLauncher.GameLauncherStatic.GameLaunchType == GameLaunchEnum.FullStart) StartPreviewGame();
       if(GameLauncher.GameLauncherStatic.GameLaunchType == GameLaunchEnum.OnlyGame) StartGameWithoutPreview();
    }

    public void StartPreviewGame()
    {
        gameStatus = GameStatus.PreviewAnimation;

        Instance.gameLinks.ropeController.StartMenuPreviewAnimation();
        Instance.gameLinks.ballField.BallFieldMenuAnimation.ResetAnimation();
    }

    private void StartGameWithoutPreview()
    {
        Instance.gameLinks.ropeController.StartMenuWithoutPreviewAnimation();
        Instance.gameLinks.ballField.BallFieldMenuAnimation.ResetAnimation();

        StartCoroutine(Wait());

        IEnumerator Wait()
        {
            yield return Instance.gameLinks.ballField.BallFieldMenuAnimation.StartAnimationViewBallField();

            StartGame();
        }
    }

    public void PrivewAnimationEnd()
    {
        gameStatus = GameStatus.WaitStartGame;
    }

    public void OnClickStartGameFromPreviewGame()
    {
        StartCoroutine(WaitEndStartButtonAnimation());
    }
   
    public void GameOver()
    {
        gameStatus = GameStatus.GameOver;

        StartCoroutine(WaitEndGameOverAnimation());   
    }

    private IEnumerator WaitEndGameOverAnimation() 
    {
        yield return Instance.gameLinks.ropeController.StartGameOverAnimaiton();
        yield return Instance.gameLinks.ballField.BallFieldMenuAnimation.StartAnimationGameOver();

        Instance.gameLinks.canvasLinks.gameOverUIPanel.Show();
    }

    private IEnumerator WaitEndStartButtonAnimation()
    {
        yield return Instance.gameLinks.ropeController.EndMenuPreviewAnimation();
        yield return Instance.gameLinks.ballField.BallFieldMenuAnimation.StartAnimationViewBallField();

        StartGame();
    }

    private void StartGame()
    {
        Instance.gameLinks.ballField.BuildField();

        Instance.gameLinks.ropeController.StartGameAfterAnimation();

        gameStatus = GameStatus.GameAlive;
    }

    public void RestartGame()
    {
        DOTween.KillAll();

        GameLauncher.GameLauncherStatic.ChangeGameLaunchType(GameLaunchEnum.OnlyGame);

        SceneManager.LoadScene(0);
    }

    public void InGameMenu()
    {
        DOTween.KillAll();

        GameLauncher.GameLauncherStatic.ChangeGameLaunchType(GameLaunchEnum.FullStart);

        SceneManager.LoadScene(0);
    }
}
