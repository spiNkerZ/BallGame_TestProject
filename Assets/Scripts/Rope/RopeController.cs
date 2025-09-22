using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private RopeAmplitudeAnimation ropeAmplitudeAnimation;
    [SerializeField] private RopeInteractionObjects ropeInteractionObjects;
    [SerializeField] private RopePlayerControl ropePlayerControl;
    [SerializeField] private RopeMenuAnimation ropeMenuAnimation;
    
    public void StartMenuPreviewAnimation()
    {
        ropeMenuAnimation.StartMenuAnimation();
    }

    public void StartMenuWithoutPreviewAnimation()
    {
        ropeMenuAnimation.StartMenuWithoutAnimation();
    }

    public IEnumerator EndMenuPreviewAnimation()
    {
        yield return ropeMenuAnimation.PlayAnimationEnd();
    }

    public IEnumerator StartGameOverAnimaiton()
    {
        yield return ropeAmplitudeAnimation.StopAmplitudeAnimation();
         
        StartCoroutine(ropeMenuAnimation.PlayAnimationGameOver());
    }

    public void StartGameAfterAnimation()
    {
        Reload();
    }

    public void Reload()
    {
        ropeAmplitudeAnimation.SubsrabeToAmplitudeTick(AmpltideTickCompleted);
    }

    private void AmpltideTickCompleted()
    {
        RopeObjectSO spawnRopeObjectPreset = GameInstance.Instance.levelData.selectionRopeObject.GetRandomElementList();

        ropeInteractionObjects.SpawnRopeObject(spawnRopeObjectPreset.Create(), spawnRopeObjectPreset.amplitudeData, true);
        ropeAmplitudeAnimation.UnSubsrabeToAmplitudeTick(AmpltideTickCompleted);
    }

    private void Update()
    {
        GameStatus gameStatus = GameInstance.Instance.gameController.GameStatus;

        if (gameStatus != GameStatus.GameAlive) return;

        if (Input.GetMouseButtonDown(0)) ropePlayerControl.PlayerTap();
    }
}
