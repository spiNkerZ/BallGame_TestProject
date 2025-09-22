using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BallField : MonoBehaviour
{
    public event Action<CellData[,]> OnBallFieldUpdate;

    public int countCellX, countCellY;

    [SerializeField] Transform startCellPoint;

    [SerializeField] private float startCellsOffset;

    [SerializeField] SpriteRenderer fieldSprite;

    [SerializeField] GameObject testPointPrefab;

    [SerializeField] BallFieldMenuAnimation ballFieldMenuAnimation;

    private CellData[,] cellsData;

    public BallFieldMenuAnimation BallFieldMenuAnimation => ballFieldMenuAnimation;


    public void BuildField()
    {
        cellsData = new CellData[countCellX, countCellY];

        Vector2 startFieldPoint = startCellPoint.position;
        Vector2 sizeFieldOffset = fieldSprite.bounds.size / countCellX;

        int ID = 0;

        for (int x = 0; x < countCellX; x++)
        {
            for (int y = 0; y < countCellY; y++)
            {  
                Vector2 cellPosition = startFieldPoint + new Vector2(x * sizeFieldOffset.x, y * sizeFieldOffset.y);

                cellsData[x, y] = new(ID, cellPosition);

                ID++;
            }
        }

#if UNITY_EDITOR

        //for (int x = 0; x < countCellX; x++)
        //{
        //    for (int y = 0; y < countCellY; y++)
        //    {
        //         Instantiate(testPointPrefab, cellsData[x, y].position, Quaternion.Euler(0,0,0)); 
        //    }
        //}
#endif
    }

    public bool CheckCellForFly(RopeObject _ropeObject)
    {
        return !GetFreeCellForFly(_ropeObject).CellIsFree;
    }

    public CellData GetFreeCellForFly(RopeObject _ropeObject)
    {
        CellData cellDataSelect = null;

        Dictionary<int,float> cellsRows = new();

        for (int x = 0; x < countCellX; x++)
        {
            cellsRows.Add(x, cellsData[x,0].GetDistToThisCell(_ropeObject.transform.position));
        }

        var minPair = cellsRows.Aggregate((l, r) => l.Value < r.Value ? l : r);
        int nearCellX = minPair.Key;
        float minValue = minPair.Value;

        for (int y = 0; y < countCellY; y++)
        {
            cellDataSelect = cellsData[nearCellX, y];

            if(cellDataSelect.CellIsFree)
            {
                break;
            }
        }

        return cellDataSelect;
    }

    public void OccupyCurrectCell(CellData _cellData, RopeObject _ropeObject)
    {
        _cellData.Occupy(_ropeObject);

        OnBallFieldUpdate?.Invoke(cellsData);
    }
}
