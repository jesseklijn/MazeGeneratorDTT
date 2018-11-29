using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    #region fields

    //Holds dimensions of the size of the grid
    private int xSize, ySize;

    //Holds all the vertices of the grid
    public List<GameObject> tileGrid;

    //Contains the prefab that we generate
    public GameObject tilePrefab;


    //Holds tiles and parent location
    private Transform gridParent;

    #endregion

    #region setup
    //Constructor
    public void Init()
    {
        tileGrid = new List<GameObject>();
        Generate(xSize, ySize);
        Debug.Log("Generated");
    }

    public void SetDimensions(int x, int y)
    {
        Debug.Log("Setup dimensions");
        this.xSize = x;
        this.ySize = y;
    }

    #endregion

    //Contains logic for generating a tile grid
    #region generation & destructor
    //Generates a grid of tiles
    void Generate(int xSize, int ySize)
    {

        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                //instantiate and keep the tile in a list for reference 
                GameObject tile = Instantiate(tilePrefab, new Vector3(i,transform.position.y,j),Quaternion.identity, transform);
                tileGrid.Add(tile);
               
            }

        }
    }



    #endregion


    }
