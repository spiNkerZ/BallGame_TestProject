using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BallPreset_RopeObject", menuName = "RopeObjects/BallPreset")]
public class BallPreset_RopeObjectSO : RopeObjectSO
{
    public BallTypeEnum ballType;

    [SerializeField] Color color;

    [SerializeField] private int countPointsAdd;

    public Color GetObjectColor()
    {
        return color;
    }

    public int GetCountPoints()
    {
        return countPointsAdd;
    }
}
