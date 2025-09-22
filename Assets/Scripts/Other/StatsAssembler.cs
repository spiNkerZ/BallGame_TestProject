using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsAssembler : MonoBehaviour
{
    public int CountPoints
    {
        get { return countPoints; }
    }

    private int countPoints;

    public void AddPoints(int _countPoints)
    {
        countPoints++;
    }
    
}
