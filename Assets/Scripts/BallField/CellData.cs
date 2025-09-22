using UnityEngine;

public class CellData
{
    public bool CellIsFree => ropeObjectInThisCell == null;

    public int ID;
    public RopeObject ropeObjectInThisCell;
    public Vector2 position;

    public CellData(int _ID, Vector2 _pos)
    {
        ID = _ID;
        position = _pos;    
    }

    public void Occupy(RopeObject _ropeObject)
    {
        ropeObjectInThisCell = _ropeObject;
    }

    public void UnOccupy()
    {
        ropeObjectInThisCell = null;
    }

    public float GetDistToThisCell(Vector2 _pos)
    {
        return Vector2.Distance(_pos, position);
    }
}
