using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    #region fields
    //Holds dimensions of the size of the grid
    private int xSize = 0, ySize = 0;
    //Holds all the vertices of the grid
    public List<Tile> tileGrid;
    #endregion 

    //Constructor
    public Grid(int xSize, int ySize)
    {
        tileGrid = new List<Tile>();
        Generate(xSize, ySize);
    }

    //Contains logic for generating a tile grid
    #region generation
    //Generates a grid of tiles
    void Generate(int xSize, int ySize)
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
               tileGrid.Add(new Tile(new Vector3(i,0,j)));
               //Debug.Log("added a tile on[" + i + "|" + j + "].");
            }

        }
    }
    #endregion

   
}
