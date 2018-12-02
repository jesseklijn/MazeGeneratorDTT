using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    #region fields

    //Holds dimensions of the size of the grid
    private int xSize, ySize;

    //Holds all the gameObjects of the grid
    public List<GameObject> tileGrid;

    //Holds all the GameObjects of the grid that are listed as edge.
    public List<GameObject> edgeList;

    //Contains the prefab that we generate
    public GameObject tilePrefab;

    //Holds entry and exit points for maze 
    //TODO: Add this as an extension of grid
    private Tile entry;
    private Tile exit;

    //Magic numbers
    public const int DIVIDER = 2;
    #endregion

    #region setup
    //Constructor
    public void Init(int seed)
    {
        ChangeSeed(seed);

        edgeList = new List<GameObject>();
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

    public void SetEdges()
    {
        Debug.Log("Setting edges of maze..");
        for (int i = 0; i < tileGrid.Count; i++)
        {
            //Checks whether the selected tile is on the edge of the maze..
            if (tileGrid[i].transform.position.x == transform.position.x ||
                tileGrid[i].transform.position.z == transform.position.z ||
                tileGrid[i].transform.position.x == (xSize - 1) ||
                tileGrid[i].transform.position.z == (ySize - 1))
            {
                //Add to a list of edges
                AddToEdgeList(tileGrid[i]);
                //If it is on the edge, mark the walls
                //TODO: add different sides of wall 
                Tile thisTile = tileGrid[i].GetComponent<Tile>();
                for (int j = 0; j < thisTile.walls.Length; j++)
                {
                    if (thisTile.neighbours[j] == null)
                    {
                        thisTile.walls[j] = true;
                    }
                }

             
            }


        }
    }

    //Based on onlly having edges at the maze, remove two tiles and verify the length
    public void SetEntryPoints()
    {
        float distance = 0;
        Debug.Log("Setting entry points");

        int entryIndex = Random.Range(0, (edgeList.Count - 1));

        int exitIndex = Random.Range(0, (edgeList.Count - 1));

        while (true)
        {

            //assign a new distance and see if the entry and exit are apart 
            distance = Vector2.Distance(edgeList[entryIndex].GetComponent<Tile>().position,
                edgeList[exitIndex].GetComponent<Tile>().position);
            Debug.Log("Distance captured: " + distance);
            if (distance > xSize / DIVIDER | distance > ySize / DIVIDER)
            {
                break;
            }
            else
            {
                entryIndex = Random.Range(0, (edgeList.Count - 1));
                exitIndex = Random.Range(0, (edgeList.Count - 1));
            }

        }

        if (distance > xSize / DIVIDER | distance > ySize / DIVIDER)
        {
            Debug.Log("Found an suitable entry [" + entryIndex + "(" + edgeList[entryIndex].transform.position + ")] and exit point[" + exitIndex + "(" + edgeList[exitIndex].transform.position + ")]");
            exit = edgeList[exitIndex].GetComponent<Tile>();
            entry = edgeList[entryIndex].GetComponent<Tile>();
            exit.isExit = true;
            entry.isEntry = true;

            //Remove the walls of one at the exit
            for (int i = 0; i < exit.walls.Length; i++)
            {
                if (exit.walls[i] == true)
                {
                    exit.walls[i] = false;
                    break;
                }

            }

            //Remove the walls of one at the entry
            for (int i = 0; i < entry.walls.Length; i++)
            {
                if (entry.walls[i] == true)
                {
                    entry.walls[i] = false;
                    break;
                }

            }

            return;

        }

        Debug.Log("Failed finding a suitable entry / exit point");

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
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, transform.position.y, j), Quaternion.identity, transform);
                //Assign a position
                tile.GetComponent<Tile>().position = new Vector2(i, j);
                //Add it to a tilegrid list for shortcut access
                tileGrid.Add(tile);

            }

        }

        //fill neighbours
        for (int i = 0; i < tileGrid.Count; i++)
        {
            //Store the tile component
            Tile tile = tileGrid[i].GetComponent<Tile>();
            //Check all four neigbour to be filled
            for (int j = 0; j < tile.neighbours.Length; j++)
            {
                tile.neighbours[j] = GetNeighbourTile(tile, j);
            }
        }

    }

    //Retrieves if possible a neighbour tile and sets it on the 
    Tile GetNeighbourTile(Tile from ,int i)
    {
        for (int j = 0; j < tileGrid.Count; j++)
        {
            switch (i)
            {
                case 0: //up
                    if (new Vector2(from.position.x,from.position.y+1) == tileGrid[j].GetComponent<Tile>().position)
                    {
                        return tileGrid[i].GetComponent<Tile>();
                    }

                    break;
                case 1: //down
                    if (new Vector2(from.position.x, from.position.y - 1) == tileGrid[j].GetComponent<Tile>().position)
                    {
                        return tileGrid[i].GetComponent<Tile>();
                    }
                    break;
                case 2: //left
                    if (new Vector2(from.position.x - 1, from.position.y) == tileGrid[j].GetComponent<Tile>().position)
                    {
                        return tileGrid[i].GetComponent<Tile>();
                    }
                    break;
                case 3: //right
                    if (new Vector2(from.position.x +1, from.position.y) == tileGrid[j].GetComponent<Tile>().position)
                    {
                        return tileGrid[i].GetComponent<Tile>();
                    }
                    break;
            }
        }
      
        //return if no results are matched
        return null;
    }

    #endregion


    //regular methods
    #region gridlogic

    public void ChangeSeed(int seed)
    {
        Random.InitState(seed);
    }

 

    public void AddToEdgeList(GameObject tileObject)
    {
        edgeList.Add(tileObject);

    }
    #endregion

}
