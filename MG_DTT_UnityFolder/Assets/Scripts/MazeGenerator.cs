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
    }
    //Generates a maze dependent on size
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
                    if (grid.tileGrid[i].GetComponent<Tile>().isWall)
                    {
                        Gizmos.color = Color.black;
                    }
                    else
                    {
                        Gizmos.color = Color.white;
                    }
                    Gizmos.DrawCube(grid.tileGrid[i].transform.position, new Vector3(1,0.1F,1));
                }
        }
    }
    #endregion

}
