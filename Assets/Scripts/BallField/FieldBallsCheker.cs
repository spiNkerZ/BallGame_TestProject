using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldBallsCheker : MonoBehaviour
{
    [SerializeField] BallField ballField;

    private void Start()
    {
        ballField.OnBallFieldUpdate += OnBallFieldUpdated;
    }

    private void OnDestroy()
    {
        ballField.OnBallFieldUpdate -= OnBallFieldUpdated;
    }

    private void OnBallFieldUpdated(CellData[,] _cellDataArray)
    {
        Debug.Log("OnBallUpdaed");

        CheckRows(_cellDataArray);
        CheckColumns(_cellDataArray);
        CheckDiagonals(_cellDataArray);
        CheckDownFreeCells(_cellDataArray);

        if(!CheckHaveFreeCells(_cellDataArray))
        {
            GameInstance.Instance.gameController.GameOver();
        }
    }


    private void CheckColumns(CellData[,] _cellDataArray)
    {
        for (int x = 0; x < ballField.countCellX; x++)
        {
            List<CellData> cellsInColumn = new List<CellData>();

            for (int y = 0; y < ballField.countCellY; y++)
            {
                CellData cellData = _cellDataArray[x, y];

                if (!cellData.CellIsFree)
                {
                    cellsInColumn.Add(cellData);
                }
                else
                {
                    if (cellsInColumn.Count >= 3)
                    {
                        CheckArrayCells(cellsInColumn);
                    }
                    cellsInColumn.Clear();
                }
            }

            if (cellsInColumn.Count >= 3)
            {
                CheckArrayCells(cellsInColumn);
            }
        }
    }

    private void CheckRows(CellData[,] _cellDataArray)
    {
        for (int x = 0; x < ballField.countCellX; x++)
        {
            List<CellData> cellsInColumn = new List<CellData>();

            for (int y = 0; y < ballField.countCellY; y++)
            {
                CellData cellData = _cellDataArray[y, x];

                if (!cellData.CellIsFree)
                {
                    cellsInColumn.Add(cellData);
                }
                else
                {
                    if (cellsInColumn.Count >= 3)
                    {
                        CheckArrayCells(cellsInColumn);
                    }
                    cellsInColumn.Clear();
                }
            }

            if (cellsInColumn.Count >= 3)
            {
                CheckArrayCells(cellsInColumn);
            }
        }
    }


    private void CheckArrayCells(List<CellData> _cellsArrayInRow)
    {
        if (_cellsArrayInRow.Count < ballField.countCellX) return;

        BallPreset_RopeObjectSO referenceBall = (BallPreset_RopeObjectSO)_cellsArrayInRow.FirstOrDefault().ropeObjectInThisCell.preset;

        if (_cellsArrayInRow.All(t => ((BallPreset_RopeObjectSO)t.ropeObjectInThisCell.preset).ballType == referenceBall.ballType))
        {
            foreach (var item in _cellsArrayInRow)
            {
                item.ropeObjectInThisCell.DestroyThisObject();
                item.UnOccupy();
            }
        }
    }

    private void CheckDiagonals(CellData[,] _cellDataArray)
    {
        int width = ballField.countCellX;
        int height = ballField.countCellY;

        for (int startX = 0; startX < width; startX++)
        {
            for (int startY = 0; startY < height; startY++)
            {
                List<CellData> diag = new List<CellData>();
                int x = startX;
                int y = startY;

                while (x < width && y < height)
                {
                    CellData cell = _cellDataArray[x, y];
                    if (!cell.CellIsFree)
                    {
                        diag.Add(cell);
                    }
                    else
                    {
                        if (diag.Count >= 3) CheckArrayCells(diag);
                        diag.Clear();
                    }

                    x += 1;
                    y += 1;
                }

                if (diag.Count >= 3) CheckArrayCells(diag);
            }
        }

        for (int startX = 0; startX < width; startX++)
        {
            for (int startY = 0; startY < height; startY++)
            {
                List<CellData> diag = new List<CellData>();
                int x = startX;
                int y = startY;

                while (x < width && y >= 0)
                {
                    CellData cell = _cellDataArray[x, y];
                    if (!cell.CellIsFree)
                    {
                        diag.Add(cell);
                    }
                    else
                    {
                        if (diag.Count >= 3) CheckArrayCells(diag);
                        diag.Clear();
                    }

                    x += 1;
                    y -= 1;
                }

                if (diag.Count >= 3) CheckArrayCells(diag);
            }
        }
    }


    private void CheckDownFreeCells(CellData[,] _cellDataArray)
    {
        for (int x = 0; x < ballField.countCellX; x++)
        {
            for (int y = 0; y < ballField.countCellY; y++)
            {
                if (_cellDataArray[x, y].CellIsFree)
                {
                    for (int yy = y + 1; yy < ballField.countCellY; yy++)
                    {
                        if (!_cellDataArray[x, yy].CellIsFree)
                        {
                            var source = _cellDataArray[x, yy];
                            var target = _cellDataArray[x, y];

                            target.Occupy(source.ropeObjectInThisCell);
                            source.UnOccupy();

                            if (target.ropeObjectInThisCell != null)
                            {
                                target.ropeObjectInThisCell.transform.DOMove(target.position, 1).SetEase(Ease.OutBounce);
                            }

                            break;
                        }
                    }
                }
            }
        }
    }

    private bool CheckHaveFreeCells(CellData[,] _cellDataArray)
    {
        int countFree = 0;

        for (int x = 0; x < ballField.countCellX; x++)
        {
            for (int y = 0; y < ballField.countCellY; y++)
            {
                if (_cellDataArray[x, y].CellIsFree) countFree++;
            }
        }

        return countFree > 0;
    }
}
