using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPointsAdder : MonoBehaviour
{
    [SerializeField] AddPointsTextView AddPointsTextViewPrefab;

    public void AddPoints(int _countPoints)
    {
        AddPointsTextViewPrefab.CreateAndPlayAnimation(_countPoints, transform.position);

        GameInstance.Instance.gameLinks.statsAssembler.AddPoints(_countPoints);
    }
}
