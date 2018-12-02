using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class MazeGenerator : MonoBehaviour
{
    public GameObject gridPrefab;
    public GameObject gridObject;
    public Grid grid;
    public int x = 10, y = 10;
    public int seed = 0;

    #region Maze Methods & Logic

    //Generates a maze dependent on size
    public void Generate(int x, int y)
    {

        //Step 0 check if maze isn't already generated
        if (gridObject != null)
        {
            DestroyMaze();
        }

        //Comment if testing
        seed = Random.Range(-10000000, 10000000);


        //Step 1 generate a grid

        gridObject = Instantiate(gridPrefab, transform.position, Quaternion.identity, transform);


        grid = gridObject.AddComponent<Grid>();
        grid.SetDimensions(x, y);
        grid.Init(seed);


        //Step 2 create edges on the mazes
        grid.SetEdges();

        //Step 3 initialize random state and assign entry points

        grid.SetEntryPoints();

        //Step 4 start the maze generator to generate a perfect maze.
        //while (true)
        //{
        //    if (AlgorithmDivision() == true)
        //    {
        //        break;
        //    }

        //}
    }

    //public bool AlgorithmDivision()
    //{
    //    if (AlgorithmDivision() == true)
    //    {
    //        return true;
    //    }
    //    else
    //    {

    //    }
    //}


    //Destroys a maze
    public void DestroyMaze()
    {

        DestroyImmediate(gridObject);

    }
    #endregion

    //Contains all unity methods
    #region standard unity methods

    //Called when this game object is enabled
    void OnEnable()
    {

    }

    //Called when this game object is disabled
    void OnDisable()
    {

    }

    #endregion

    #region gizmo debug
    void OnDrawGizmos()
    {
        if (grid != null)
        {
            if (grid.tileGrid.Count > 0)
                for (int i = 0; i < grid.tileGrid.Count; i++)
                {
                    Tile tile = grid.tileGrid[i].GetComponent<Tile>();
                    if (tile.isEntry == true)
                    {
                        Gizmos.color = Color.green;

                    }
                    else if (tile.isExit == true)
                    {
                        Gizmos.color = Color.cyan;
                    }
                    else
                    {

                        Gizmos.color = Color.white;

                    }

                    Gizmos.DrawCube(grid.tileGrid[i].transform.position, new Vector3(1, 0.1F, 1));

                    Gizmos.color = Color.black;
                    Vector3 tilePos = grid.tileGrid[i].transform.position;
                    
                
                    tilePos = new Vector3(tilePos.x - (tile.size/2), tilePos.y + 0.1F, tilePos.z - (tile.size/2));
                    for (int j = 0; j < tile.walls.Length; j++)
                    {
                        if (tile.walls[j] == true)
                        {
                            switch (j)
                            {
                                case 0: //up
                                    Gizmos.DrawLine(new Vector3(tilePos.x,tilePos.y,tilePos.z+tile.size), new Vector3(tilePos.x + tile.size, tilePos.y, tilePos.z + tile.size));
                                    break;
                                case 1: //down
                                    Gizmos.DrawLine(tilePos, new Vector3(tilePos.x + tile.size, tilePos.y, tilePos.z));
                                    break;
                                case 2: //left
                                    Gizmos.DrawLine(tilePos, new Vector3(tilePos.x , tilePos.y, tilePos.z + tile.size));
                                    break;
                                case 3: //right
                                    Gizmos.DrawLine(new Vector3(tilePos.x + tile.size, tilePos.y, tilePos.z), new Vector3(tilePos.x + tile.size, tilePos.y, tilePos.z + tile.size));
                                    break;
                            }
                        }
                    }
                    


                }
        }
    }
    #endregion

}
