using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    //Walls
    //                      up      down    left    right
    public bool[] walls = { true, true, true, true };

    //Tile neighbour locations
    public Tile[] neighbours = { null, null, null, null };

    //Tile size
    public float size = 1;

    //Position on the grid
    public Vector2 position;

    //Entry and exit data
    public bool isEntry = false, isExit = false;

    public bool isVisited = false;

    public void AddWall(int direction)
    {
        walls[direction] = true;
    }
    public void RemoveWall(int direction)
    {
        walls[direction] = false;
        switch (direction) //  0 up     1 down   2   left  3  right
        {
            case 0:
                if (neighbours[direction] != null)
                {
                    neighbours[direction].walls[1] = false;
                }
                break;
            case 1:
                if (neighbours[direction] != null)
                {
                    neighbours[direction].walls[0] = false;
                }
                break;
            case 2:
                if (neighbours[direction] != null)
                {
                    neighbours[direction].walls[3] = false;
                }
                break;
            case 3:
                if (neighbours[direction] != null)
                {
                    neighbours[direction].walls[2] = false;
                }
                break;
                
        }
    }
    //Returns neighbours that are not visited and not null
    public List<int> HasNeighbours()
    {
        List<int> neighbours = new List<int>();
        for (int i = 0; i < this.neighbours.Length; i++)
        {
            if (this.neighbours[i] != null)
            {
                if (this.neighbours[i].isVisited == false)
                {
                    neighbours.Add(i);
                }
            }
        }

        return neighbours;
    }

}
