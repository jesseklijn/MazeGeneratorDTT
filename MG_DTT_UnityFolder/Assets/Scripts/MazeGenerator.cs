using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class MazeGenerator : MonoBehaviour
{
    public GameObject gridPrefab;
    public GameObject gridObject;
    public Grid grid;
    [Range(2,35)]
    public int x = 10, y = 10;
    public int seed = 0;

    //Marks those who are visited
    public List<Tile> visited;

    public Stack<Tile> stack;


    #region Maze Methods & Logic

    //Generates a maze dependent on size
    public void Generate(int x, int y)
    {

        //Step 0 check if maze isn't already generated
        if (gridObject != null)
        {
            DestroyMaze();
           
        }

        //Reset some data
        stack = new Stack<Tile>();
        visited = new List<Tile>();

        //Change the seed
        seed = Random.Range(0,(int)System.DateTime.Now.Ticks);

        //init the random here and in grid
        Random.InitState(seed);

        //Step 1 generate a grid

        gridObject = Instantiate(gridPrefab, transform.position, Quaternion.identity, transform);


        grid = gridObject.AddComponent<Grid>();
        grid.SetDimensions(x, y);
        grid.Init(seed);


        //Step 2 create edges on the mazes
        grid.SetEdges();

        //Step 3 initialize random state and assign entry points

        grid.SetEntryPoints();
        visited.Add(grid.entry);
        stack.Push(grid.entry);
        grid.entry.isVisited = true;
        //Step 4 start the maze generator to generate a perfect maze.
        while (visited.Count != (x * y))
        {

            Solver();
            //Random.InitState((int)System.DateTime.Now.Ticks);
        }
    }

    public void Solver()
    {

      
        //Get the current cell
        Tile currentTile = stack.Peek();
        //Debug.Log("Current tile: [" + currentTile.position +"]");
        //Get neighbours from this cell that are not visited yet
        List<int> neighbours = currentTile.HasNeighbours();
        //Debug.Log("Has "+neighbours.Count+" available.");
        //Check if there are any neighbours unvisited
        if (neighbours.Count > 0)
        {
            //Choose a random index from the collection of available neighbours

            //Debug.Log("I chose between: 0 and " + (neighbours.Count));

            int i = Random.Range(0, neighbours.Count);
            //Debug.Log("Has chosen " + i + " as neighbour to visit.");
            //Debug.Log("Trying it myself.." + Random.Range(0, neighbours.Count));
            currentTile.RemoveWall(neighbours[i]);
            stack.Push(currentTile.neighbours[neighbours[i]]); //-1 to get index
          



            //mark visited
            //Debug.Log("Added to visited and set visit to true.");
            visited.Add(stack.Peek());
            stack.Peek().isVisited = true;
          
        }
        else
        {
            stack.Pop(); //backtracks
          
        }

       
    }

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
