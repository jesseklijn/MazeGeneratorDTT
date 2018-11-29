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

    //Holds all the GameObjects of the grid that are listed as wall.
    public List<GameObject> wallList;

    //Contains the prefab that we generate
    public GameObject tilePrefab;


    //Holds tiles and parent location
    private Transform gridParent;

    //Magic numbers
    public const int DIVIDER = 2;
    #endregion

    #region setup
    //Constructor
    public void Init(int seed)
    {
        ChangeSeed(seed);

        wallList = new List<GameObject>();
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
                //If it is on the edge, mark it as a wall and add it to the wall list.
                AddToWallList(tileGrid[i]);
            }


        }
    }

    //Based on onlly having edges at the maze, remove two tiles and verify the length
    public void SetEntryPoints()
    {
        float distance = 0;
        Debug.Log("Setting entry points");

        int attempts = 0;
        int entryIndex = Random.Range(0, (wallList.Count - 1));
        while (attempts < 1000)
        {
            attempts++;
            if (wallList[entryIndex].transform.position.x == transform.position.x &
                wallList[entryIndex].transform.position.z == transform.position.z || //0,0
                wallList[entryIndex].transform.position.x == transform.position.x &
                wallList[entryIndex].transform.position.z == transform.position.z + ySize-1 || //0,3
                wallList[entryIndex].transform.position.x == transform.position.x + xSize-1 &
                wallList[entryIndex].transform.position.z == transform.position.z + ySize-1 || //3,3
                wallList[entryIndex].transform.position.x == transform.position.x + xSize-1 &
                wallList[entryIndex].transform.position.z == transform.position.z) //3,0
            {
                
                entryIndex = Random.Range(0, (wallList.Count - 1));
            }
            else
            {
                Debug.Log(wallList[entryIndex].transform.position.x + "|" + wallList[entryIndex].transform.position.z);
                break;
            }
        }
        Debug.Log("attempts made:" + attempts);
        attempts = 0;
        Debug.Log(entryIndex);
        int exitIndex = Random.Range(0, (wallList.Count - 1));

        attempts = 0;
        while (attempts < 200)
        {
            //count the attempts, to make sure Unity doesn't crash 
            attempts++;

            //Assign a new random index for the exit

            exitIndex = Random.Range(0, (wallList.Count - 1));
            if (wallList[exitIndex].transform.position.x == transform.position.x &
                wallList[exitIndex].transform.position.z == transform.position.z || //0,0
                wallList[exitIndex].transform.position.x == transform.position.x &
                wallList[exitIndex].transform.position.z == transform.position.z + ySize -1 || //0,3
                wallList[exitIndex].transform.position.x == transform.position.x + xSize -1&
                wallList[exitIndex].transform.position.z == transform.position.z + ySize -1 || //3,3
                wallList[exitIndex].transform.position.x == transform.position.x + xSize -1 &
                wallList[exitIndex].transform.position.z == transform.position.z) //3,0
                continue;
            else
            {
                //assign a new distance and see if the entry and exit are apart 
                distance = Vector3.Distance(wallList[entryIndex].transform.position,
                    wallList[exitIndex].transform.position);
                Debug.Log("Distance captured: " + distance);
                if (distance > xSize / DIVIDER | distance > ySize / DIVIDER)
                {
                    break;
                }
            }

            
        }

        if (distance > xSize / DIVIDER | distance > ySize / DIVIDER)
        {
            Debug.Log("Found an suitable entry [" + entryIndex + "(" + wallList[entryIndex].transform.position + ")] and exit point[" + exitIndex + "(" + wallList[exitIndex].transform.position + ")]");
            GameObject exit = wallList[exitIndex];
            GameObject entry = wallList[entryIndex];
            RemoveWallListItem(entry);
            RemoveWallListItem(exit);

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
                tileGrid.Add(tile);

            }

        }
    }



    #endregion


    //regular methods
    #region gridlogic

    public void ChangeSeed(int seed)
    {
        Random.InitState(seed);
    }

    public void AddToWallList(GameObject tileObject)
    {
        tileObject.GetComponent<Tile>().isWall = true;
        wallList.Add(tileObject);

    }

    public void RemoveWallListItem(GameObject tileObject)
    {
        tileObject.GetComponent<Tile>().isWall = false;
        wallList.Remove(tileObject);

    }
    #endregion

}
