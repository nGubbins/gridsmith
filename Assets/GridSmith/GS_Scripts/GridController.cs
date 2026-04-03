using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GridSmith{
public class GridController : MonoBehaviour
{
    //ToDo: option to match parent scale on spawn
    //ToDo: allow for separate X and Y offset, option to auto get offsets from GridTile size
    //ToDo: option for chessboard, columns, or rows, currently defaults to chessboard
    //ToDo [DONE]: setup editor options and test buttons
    public GameObject GridTile;
    public GameObject GridTileAlt; 
    public bool UseAlt = true;
    public Vector2Int Dimensions;
    //int dimX = 5;
    //int dimY = 5;
    public Vector2 Offset;
    public bool LoadOnStart = true;
    public GameObject[,] GridObjects;

    void Start()
    {
        if(LoadOnStart)
            SetUp();
    }

    //draw grid to the screen
    public void SetUp()
    {
        int dimX = Dimensions.x;
        int dimY = Dimensions.y;
        GridObjects = new GameObject[dimX,dimY];

        for(int i = 0; i < dimX; ++i)
        {
            for(int j = 0; j < dimY; ++j){
                Vector3 spawnPos = transform.position;
                spawnPos.x += (i - (dimX/2)) * (1 + Offset.x);
                spawnPos.y += (j - (dimY/2)) * (1 + Offset.y);
                GameObject curObj = null;
                if(!UseAlt){
                    curObj = Instantiate<GameObject>(GridTile, spawnPos, Quaternion.identity);
                }
                else
                {
                    if((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0)){
                      curObj =   Instantiate<GameObject>(GridTile, spawnPos, Quaternion.identity);}
                    else{
                      curObj =   Instantiate<GameObject>(GridTileAlt, spawnPos, Quaternion.identity);}

                }
                curObj.transform.parent = gameObject.transform;
                GridObjects[i,j] = curObj;
            }
        }
    }

    /***Swap the position of two grid objects by index (int[,],int[,])
    Overload true to only allow neighbour swaps.
    Returns true on a successful swap.***/
    public bool SwapByCoordinates(Vector2Int tile1, Vector2Int tile2, bool neighboursOnly = false)
    {

        if(neighboursOnly) //In this logic, diagonals are NOT NEIGHBOURS
        {
            int dist = Math.Abs(tile1.x - tile2.x) + Math.Abs(tile1.y - tile2.y);
            if (dist != 1) 
                return false;
        }
        //store objects
        GameObject obj1 = GridObjects[tile1.x, tile1.y];
        GameObject obj2 = GridObjects[tile2.x, tile2.y];

        //swap world positions
        Vector3 tempPos = obj1.transform.position;        
        obj1.transform.position = obj2.transform.position;
        obj2.transform.position = tempPos;

        //swap array references
        GridObjects[tile2.x, tile2.y] = obj1;
        GridObjects[tile1.x, tile1.y] = obj2;

        return true;
    }

   /* public void SwapByObject(GameObject tile1, GameObject tile2, bool neighboursOnly = false)
    {
        //ToDo: Swap given objects
        //update GirdObjects[] to match new positions
    } */

    //ToDo: Group Select & Group Swap

}
}
