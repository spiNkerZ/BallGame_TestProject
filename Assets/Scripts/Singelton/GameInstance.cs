using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance;

    public GameController gameController;
    public GameLinks gameLinks;
    public LevelData levelData;

    private void Awake()
    {
        if(GameInstance.Instance == null) GameInstance.Instance = this;
    }

    private void OnDestroy()
    {
        if(GameInstance.Instance) Destroy(GameInstance.Instance.gameObject);
    }
}
