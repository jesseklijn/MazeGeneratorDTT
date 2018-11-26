using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class MazeGenerator : MonoBehaviour
{
    public Grid grid;
    public int x = 10, y = 10;
    #region Maze Methods & Logic

    //Generates a maze dependent on size
    public void Generate(int x, int y)
    {
        grid = new Grid(x, y);
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
                    Gizmos.color = Color.black;
                    Gizmos.DrawSphere(grid.tileGrid[i].position, 0.5F);
                }
        }
    }
    #endregion

}
