using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    //Walls
    //                      up      down    left    right
    public bool[] walls = { false, false, false, false };

    //Tile neighbour locations
    public Tile[] neighbours = { null, null, null, null };

    //Tile size
    public float size = 1;

    //Position on the grid
    public Vector2 position;

    //Entry and exit data
    public bool isEntry = false, isExit = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
