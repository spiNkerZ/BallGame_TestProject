using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    public static GameLauncher GameLauncherStatic;

    public GameLaunchEnum GameLaunchType 
    {
        get {  return gameLaunchType; }
    }

    [SerializeField] private GameLaunchEnum gameLaunchType;

    private void Awake()
    {
        if (GameLauncher.GameLauncherStatic == null)
        {
            GameLauncherStatic = this;

            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void ChangeGameLaunchType(GameLaunchEnum _gameLaunchType)
    {
        gameLaunchType = _gameLaunchType;
    }

    private void OnDestroy()
    {
        if (GameLauncher.GameLauncherStatic != null)
        {
            if (GameLauncher.GameLauncherStatic.gameObject == gameObject)
            {
                GameLauncher.GameLauncherStatic = null;
            }
        }
    }
}
