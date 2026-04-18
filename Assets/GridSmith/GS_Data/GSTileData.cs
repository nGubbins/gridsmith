using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GSTileData
{
    public GSTileData(int x, int y)
    {
        name = x.ToString() + "-" + y.ToString(); //ToDo: name shouldn't change on swap, give name not based on coordinates (index number)
        coordinates.x = x;
        coordinates.y = y;
        startpoint = coordinates; //ToDo: startpoint shouldn't change on swap
        occupied = false;
        wall = false;
        //neighbours
    }
    public string name = "";
    public Vector2Int startpoint;
    public Vector2Int coordinates;
    public bool occupied = false;
    public bool wall = false;
    public List<Vector2Int> neighbours;
}
